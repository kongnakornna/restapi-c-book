using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HttpClients
{
    public class HttpResponseHandler : DelegatingHandler
    {
        private readonly ILogger<HttpResponseHandler> logger;

        public HttpResponseHandler(ILogger<HttpResponseHandler> logger)
        {
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var resp = await base.SendAsync(request, cancellationToken);

                if (((int)resp.StatusCode) < 200 || ((int)resp.StatusCode) > 299)
                {
                    var reqDetail = $"{request.Method}:{request.RequestUri.ToString()} Body:{(request.Content?.ReadAsStringAsync().Result == "" ? "n/a" : request.Content?.ReadAsStringAsync().Result)}";
                    var logMsg = $"{reqDetail} fail.Repose {resp.StatusCode} [{(int)resp.StatusCode}]. {await resp.Content?.ReadAsStringAsync()}";
                    this.logger.LogError(logMsg);
                    throw new System.Exception(logMsg);
                }

                return resp;
            }
            catch (System.Exception ex)
            {
                _ = ex;
                throw;
            }
        }
    }
}
