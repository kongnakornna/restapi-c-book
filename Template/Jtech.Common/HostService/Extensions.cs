using Amazon.Runtime.Internal.Util;
using Jtech.Common.HostService.Cronjob;
using Jtech.Common.HostService.FileWatcher;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HostService
{
    internal static class Extensions
    {
        public static IServiceCollection AddCronJob(this IServiceCollection services,Action<CronjobSettings> actConfigure )
        {
            CronjobSettings crons = new CronjobSettings();
            if (actConfigure != null)
                actConfigure.Invoke(crons);

            services.AddHostedService<CronJobHostService>(provider => {
                var logger = provider.GetService<ILogger<CronJobHostService>>();
                return new CronJobHostService(logger, crons);
            });
            return services;
        }
        public static IServiceCollection AddFileWatcher(this IServiceCollection services, Action<FileWacherSettings> actConfigure)
        {
            FileWacherSettings settings = new FileWacherSettings();
            if (actConfigure != null)
                actConfigure.Invoke(settings);

            
            services.AddHostedService<FileWatcherHostService>(provider => {
                var logger = provider.GetService<ILogger<FileWatcherHostService>>();
                return new FileWatcherHostService(logger, settings);
            });
           
            return services;
        }
    }
}