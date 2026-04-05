using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Confluent.Kafka;
using Jtech.Common.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HttpClients.Clients
{
    public class ForwardRequestClient
    {
        private readonly HttpClient _client;
        public ForwardRequestClient(HttpClient client)
        {
            this._client = client;
        }

        public async Task Forward(HttpContext context,Uri BaseAddress,string RelativePath)
        {
           

             var targetUri = BuildTargetUri(context.Request, BaseAddress,RelativePath);
            if (targetUri != null)
            {
             
                if (!Uri.IsWellFormedUriString(targetUri.ToString(), UriKind.Absolute))
                {
                    //To Local with out Logic
                    context.Response.Redirect(targetUri.ToString());
                    return;
                }
                else
                {
                    var targetRequestMessage = CreateTargetMessage(context, targetUri);

                    //var intercept = this._provider.GetService<IReverseProxyInterception>();
                    //if (intercept != null)
                    //    await intercept.ProcessBeforeRequest(context.Request.Path, targetRequestMessage, context);
                    //{

                    // Open the stream using a StreamReader for easy access.
                    //StreamReader reader = new StreamReader(targetRequestMessage.Content.ReadAsStream());
                    // Read the content.
                    //var result = await reader.ReadToEndAsync();
                    //reader.Close();
                    //}
                    using (var responseMessage = await this._client.SendAsync(targetRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
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
        }

        private Uri BuildTargetUri(HttpRequest request, Uri BaseAddress, string RelativePath)
        {
            return new Uri("http://lcoalhost");
            //PathString remainingPath;
            //string targetUri = null;

            //foreach (var forwardItem in this._options.ForwardList)
            //{
            //    if (request.Path.StartsWithSegments(forwardItem.StartWithPath, out remainingPath))
            //    {
            //        var forWardPath = this._options.MainHost + forwardItem.StartWithPath;

            //        if (!string.IsNullOrEmpty(forwardItem.ForWardPath))
            //            forWardPath = forwardItem.ForWardPath;

            //        targetUri = forWardPath + remainingPath + request.QueryString;

            //        break;
            //    }
            //}

            //return new Uri(targetUri);
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

    }
}
