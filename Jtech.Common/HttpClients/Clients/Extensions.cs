using Esprima.Ast;
using Jtech.Common.HttpClients;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HttpClients.Clients
{
    public static class Extensions
    {
        public static IHttpClientBuilder UseLineNotify(this IServiceCollection services, Action<HttpClientSettings>? actSetting = null)
        {
            if (actSetting == null)
            {
                actSetting = new Action<HttpClientSettings>(option =>
                {
                    option.BaseAddress = "https://notify-api.line.me/api/";
                });
            }
            return services.AddHttpClientWithPolicy<LineNotifyClient>(actSetting);
        }

        public static IServiceCollection UseEmail(this IServiceCollection services, Action<MailSettings> actSetting )
        {
            MailSettings option = new MailSettings();
            actSetting.Invoke(option);
            services.AddSingleton<JtechSMTPClient>(provider =>
            {
                return new JtechSMTPClient(option);
            });
            return services;
        }
    }
}
