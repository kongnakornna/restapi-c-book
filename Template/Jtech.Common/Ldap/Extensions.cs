using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Ldap
{
    public static class Extensions
    {
        public static IServiceCollection AddLdapProvider(this IServiceCollection services, Action<Settings> actionOption)
        {
            services.AddTransient<LdapProvider>(provider => {
                var option = new Settings();
                actionOption.Invoke(option);
                return new LdapProvider(option);
            });
            return services;
        }
    }
}
