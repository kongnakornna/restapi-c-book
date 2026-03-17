using Jtech.Common.MiddleWare;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Helpers = Jtech.Common.Helpers;

namespace Jtech.Common.BusinessLogic.Identity
{
  
    public class IdentityFromPayload : IIdentity
    {
        private readonly IHttpContextAccessor _httpContext;
        public IdentityFromPayload(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor;
        }

        public string GetUerName()
        {
            var payload = _httpContext.HttpContext!.Request!.Headers["payload"];
            if (string.IsNullOrEmpty(payload))
                return "Guest";
            else
            {
                var payloadObj = JObject.Parse(payload);
                return payloadObj == null ? "Guest" : (string)payloadObj["Data"]["user_name"];
            }
        }
    }
}
