using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using Jtech.Common.Helpers;
using Jtech.Common.Base;
using Microsoft.AspNetCore.Authorization;

namespace Jtech.Common.MiddleWare
{
    public class PayloadHandler : AuthorizationHandler <IAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SecuritySettings _setting;
        public PayloadHandler(SecuritySettings options, IHttpContextAccessor httpContextAccessor)
        {
            _setting = options;
            _httpContextAccessor = httpContextAccessor;
        }
        private JObject? GetPayload()
        {
            StringValues payloadStr;
            var req= _httpContextAccessor.HttpContext!.Request;
            if (!req.Headers.TryGetValue("payload", out payloadStr))
                return null;

            try
            {
                return Helpers.Json.DeserializeObject<JObject>(payloadStr);
            }
            catch
            {
                return null;
            }
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
        {
            if (_setting.RequirePayload)
            {
                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                var payload = this.GetPayload();
                if (payload == null)
                    context.Fail();
                else if (!payload.ContainsKey("Data"))
                    context.Fail();
                else if (!payload.ContainsKey("Sign"))
                    context.Fail();
                else if (!((JObject)payload["Data"]).ContainsKey("create_date"))
                    context.Fail();
                else if (!Helpers.Json.Serialize(payload["Data"]).ToHMAC(_setting.JwtKey).Equals(payload["Sign"].ToString()))
                    context.Fail();
                else
                {
                    var createDate =(DateTime) payload["Data"]["create_date"];

                    //check payload expire
                    if (createDate.AddMinutes(_setting.Token_Expire_Minutes) < DateTime.Now)
                        context.Fail();
                    else
                    {
                        _httpContextAccessor.HttpContext!.Response.OnStarting(state =>
                        {
                            var httpContext = (IHttpContextAccessor)state;
                            payload["Data"]["create_date"] = DateTime.Now;
                            httpContext.HttpContext!.Response.Headers.AddPayload(payload["Data"], _setting.JwtKey);

                            return Task.CompletedTask;
                        }, _httpContextAccessor);

                        context.Succeed(requirement);
                    }
                }
            }  
            await Task.CompletedTask;
        }
    }
}