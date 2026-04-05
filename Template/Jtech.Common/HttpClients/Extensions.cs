using Esprima.Ast;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Timeout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Jtech.Common.HttpClients
{
    public static class Extensions
    {
        public static IHttpClientBuilder AddHttpClientWithPolicy<T>(this IServiceCollection services,Action<HttpClientSettings> actSetting) where T : class
        {
            var provider = services.BuildServiceProvider();
            //var config = new ConfigurationHelper(provider.GetService<IConfiguration>());

            //var settings = config.GetConfigurationSection<HttpClientSettings>($"HttpClientSettings:{typeof(T).Name}");

            //if (settings == null)
            //    throw new Exception($"Not found HttpClientSettings:{typeof(T).Name} in appsettings.json");

            var options = new HttpClientSettings();
            actSetting.Invoke(options);
            var retryCount = options.RetryCountPolicy == null ? 3 : options.RetryCountPolicy;

            var client = services.AddHttpClient<T>(configure =>
            {
                if (!options.BaseAddress.EndsWith("/"))
                    options.BaseAddress += "/";
                configure.BaseAddress = new Uri(options.BaseAddress);
            });

            if (!string.IsNullOrEmpty(options.ProxyUrl))
                client.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    Proxy = new WebProxy(options.ProxyUrl)
                });

            return client.AddHttpPolicy(typeof(T), services, (int)retryCount);
        }

        internal static IHttpClientBuilder AddHttpPolicy(this IHttpClientBuilder builder, Type HttpClientType, IServiceCollection services, int RetryCount = 3) //where T : class
        {
            builder.Services.AddTransient<HttpResponseHandler>();

            builder.AddHttpMessageHandler<HttpResponseHandler>();

            builder.AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>().WaitAndRetryAsync(
                RetryCount,
                retryAttemp => TimeSpan.FromSeconds(100),
                onRetry: (outcome, timesp, retryattemp) =>
                {
                    var sp = services.BuildServiceProvider();
                    sp.GetRequiredService<ILogger<HttpClient>>().LogError($"Service {HttpClientType.Name} timeout retry count :{outcome} ...");
                }
            ))
            .AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>().CircuitBreakerAsync(
                RetryCount,
                TimeSpan.FromSeconds(5),
                onBreak: (attemp, timesp) =>
                {
                    var sp = services.BuildServiceProvider();
                    sp.GetRequiredService<ILogger<HttpClient>>().LogInformation($"Service timeout & open Circuit of client : {HttpClientType.Name} wait {timesp}...");
                },
                onReset: () =>
                {
                    var sp = services.BuildServiceProvider();
                    sp.GetRequiredService<ILogger<HttpClient>>().LogInformation($"Close Circuit of client : {HttpClientType.Name}");
                }
            ))
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(10));
            return builder;
        }

        #region For HttpClient

        internal static HttpRequestMessage CreateRequest(this HttpClient client,string relativeUrl, HttpMethod method, Action<HttpHeaders>? actHeader = null)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = method;
            if (actHeader != null)
                actHeader.Invoke(request.Headers);
            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.Substring(1);
            request.RequestUri = new Uri(client.BaseAddress, relativeUrl);
            return request;
        }

        public static async Task<T> To<T>(this HttpResponseMessage resp,JsonSerializerOptions? options=null)
        {
            return await resp.Content.ReadFromJsonAsync<T>(options);
        }
        public static async Task<HttpResponseMessage> SendJsonAsync(this HttpClient client, string relativeUrl, HttpMethod method,string jsonData,Action<HttpHeaders>? actHeader=null)
        {
            HttpRequestMessage request = client.CreateRequest(relativeUrl,method, actHeader);
            request.Content = new StringContent(jsonData);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await client.SendAsync(request);
        }

        public static async Task<U> SendJsonAsync<T,U>(this HttpClient client, string relativeUrl, HttpMethod method, T Data, Action<HttpHeaders>? actHeader = null, JsonSerializerOptions? options = null)  
        {
            HttpRequestMessage request = client.CreateRequest(relativeUrl,method, actHeader);
            request.Content = JsonContent.Create<T>(
                inputValue:Data,
                options:options
                );
            return await client.SendAsync(request).Result.To<U>(options:options);
        }

        public static async Task<HttpResponseMessage> SendFromDataAsync(this HttpClient client, string relativeUrl, HttpMethod method, string body, Action<HttpHeaders>? actHeader = null)
        {
            HttpRequestMessage request = client.CreateRequest(relativeUrl,method,actHeader);
            request.Content = new MultipartFormDataContent(body);
            return await client.SendAsync(request);
        }
        public static async Task<HttpResponseMessage> SendUrlEncodedAsync(this HttpClient client, string relativeUrl, HttpMethod method, IEnumerable<KeyValuePair<string,string>> body, Action<HttpHeaders>? actHeader = null)
        {
            HttpRequestMessage request = client.CreateRequest(relativeUrl,method, actHeader);
            request.Content = new FormUrlEncodedContent(body);
            return await client.SendAsync(request);
        }

        public static async Task<HttpResponseMessage> SendFromDataAsync(this HttpClient client, string relativeUrl, HttpMethod method, IEnumerable<KeyValuePair<string, string>> body, Action<HttpHeaders>? actHeader = null)
        {
            HttpRequestMessage request = client.CreateRequest(relativeUrl,method, actHeader);

            request.Content = new FormUrlEncodedContent(body);
            return await client.SendAsync(request);
        }

        public static async Task<HttpResponseMessage> SendStreamDataAsync(this HttpClient client, string relativeUrl, HttpMethod method, Stream body, Action<HttpHeaders>? actHeader = null)
        {
            HttpRequestMessage request = client.CreateRequest(relativeUrl,method, actHeader);
            request.Content = new StreamContent(body);
            return await client.SendAsync(request);
        }
        #endregion
    }
}
