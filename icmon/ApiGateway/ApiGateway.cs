using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.ApiGateway
{
    public class ApiGateway
    {
        private readonly HttpClient _client;
        private readonly RequestDelegate _nextMiddleware;
        private readonly GatewaySettings _setting;
        public IServiceProvider _provider;


        public ApiGateway(RequestDelegate nextMiddleware, HttpClient client,GatewaySettings setting)
        {
            _nextMiddleware = nextMiddleware;
            _client = client;
            _setting = setting;
        }

        public async Task Invoke(HttpContext context)
        {
            PathString? pathRemain = null;

            var forwardItem = GetForwardItem(context.Request,out pathRemain);
            var targetUrl = this.GetForwardUrl(forwardItem, pathRemain, context.Request.QueryString);

            if (string.IsNullOrEmpty(targetUrl))
                await _nextMiddleware(context);
            else if (!Uri.IsWellFormedUriString(targetUrl, UriKind.Absolute))
            {
                context.Response.Redirect(new Uri(targetUrl).OriginalString);
                return;
            }
            else
            {

                var targetRequestMessage = CreateTargetMessage(context, new Uri(targetUrl));

                using (var responseMessage = await _client.SendAsync(targetRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
                {
                    context.Response.StatusCode = (int)responseMessage.StatusCode;
                    CopyFromTargetResponseHeaders(context, responseMessage);

                    //if (intercept != null)
                    //    await intercept.ProcessResponse(responseMessage, context);

                    await responseMessage.Content.CopyToAsync(context.Response.Body);
                }
                return;
            }
        }

        private HttpRequestMessage CreateTargetMessage(HttpContext context, Uri targetUri)
        {
            var requestMessage = new HttpRequestMessage();
            CopyFromOriginalRequestContentAndHeaders(context, requestMessage);

            requestMessage.RequestUri = targetUri;
            requestMessage.Headers.Host = targetUri.Host;
            requestMessage.Method = new HttpMethod(context.Request.Method);

            return requestMessage;
        }

        private void CopyFromOriginalRequestContentAndHeaders(HttpContext context, HttpRequestMessage requestMessage)
        {
            var requestMethod = context.Request.Method;

            if (!HttpMethods.IsGet(requestMethod) &&
              !HttpMethods.IsHead(requestMethod) &&
              !HttpMethods.IsDelete(requestMethod) &&
              !HttpMethods.IsTrace(requestMethod))
            {
                requestMessage.Content = new StreamContent(context.Request.Body); ;
            }
           

            foreach (var header in context.Request.Headers)
            {
                switch (header.Key)
                {
                    case "Allow":
                    case "Content-Disposition":
                    case "Content-Encoding ":
                    case "Content-Language":
                    case "Content-Length":
                    case "Content-Location":
                    case "Content-MD5":
                    case "Content-Range":
                    case "Content-Type":
                    case "Expires":
                    case "Last-Modified":
                        {
                            requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                            break;
                        }
                    default:
                        {
                            requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                            break;
                        }
                }
            }
        }

        private void CopyFromTargetResponseHeaders(HttpContext context, HttpResponseMessage responseMessage)
        {
            foreach (var header in responseMessage.Headers)
                context.Response.Headers[header.Key] = header.Value.ToArray();

            foreach (var header in responseMessage.Content.Headers)
                context.Response.Headers[header.Key] = header.Value.ToArray();

            context.Response.Headers.Remove("transfer-encoding");
        }

        private ForwardItem? GetForwardItem(HttpRequest request,out PathString? remingPath)
        {
            PathString remainingPath=null;
          
            var fowardItem = this._setting.Where(x => request.Path.StartsWithSegments(x.Value.StartWithPath, out remainingPath)).FirstOrDefault();
            remingPath = remainingPath;
            if (default(KeyValuePair<string, ForwardItem>).Equals(fowardItem))
                return null;
            else
                return fowardItem.Value;
        }

        private string? GetForwardUrl(ForwardItem? item, PathString? remainingPath,QueryString? query)
        {
            if(item == null) return null;

            var forWardPath = "";

            if (!string.IsNullOrEmpty(item.ForwardPath))
                forWardPath = item.ForwardPath;
            return  forWardPath + remainingPath.ToString() + query;
        }

        private string? MathAndBuildTargetUri(HttpRequest request)
        {
            PathString remainingPath;
            string? targetUri = null;

            var fowardItem = this._setting.Where(x => request.Path.StartsWithSegments(x.Value.StartWithPath, out remainingPath)).FirstOrDefault();
            if (!default(KeyValuePair<string,ForwardItem>).Equals(fowardItem))
            {
                var item = fowardItem.Value;
                var forWardPath = "";
                if (!string.IsNullOrEmpty(item.ForwardPath))
                    forWardPath = item.ForwardPath;

                request.Path.StartsWithSegments(item.StartWithPath, out remainingPath);
                targetUri = forWardPath + remainingPath + request.QueryString;
            }

            return targetUri;
        }
    }
}
