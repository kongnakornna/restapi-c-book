using Jtech.Common.HttpClients;
using Jtech.Common.HttpClients.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.ApiGateway
{
    public static class Extensions
    {
        internal static IApplicationBuilder UseApiGW(this IApplicationBuilder app ,Func<GatewaySettings> ActSetting)
        {
        
            var setting = ActSetting.Invoke();
            app.UseMiddleware<ApiGateway>(setting);
            return app;
        }
    }
}
