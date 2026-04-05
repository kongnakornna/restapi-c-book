using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.SwaggerGen;
using Jtech.Common.Base;
using Jtech.Common.HttpClients.Clients;
using Jtech.Common.ApiGateway;
using Jtech.Common.HostService.Cronjob;
using Jtech.Common.HostService.FileWatcher;
using Jtech.Common.SystemEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Jtech.Common.Settings
{
    public class JTechSetting
    {
        internal static event AddCronjobEventHandler? onAddCronjob=null;
        internal static event AddFileWatcherSettingEventHandler? onAddFileWatcherSetting=null;
        internal static event SystemEventHandler? onSystemChange = null;

        public string UrlServiceConfiguration { get; set; } = "https://localhost:7256/WeatherForecast/configuration";
        public bool EnableSwaggerUI { get; set; } = true;
        public bool EnableAcceptCsvFormatter { get; set; } = true;
        public bool EnableAcceptXmlFormatter { get; set; } = true;

        public Action<WebApplicationBuilder>? BuilderConfigre;
        public Action<WebApplication>? AppConfigure;
        public Action<LoggerConfiguration>? LogConfigure;
        public Action<SwaggerGenOptions>? SwaggerConfigure;
        public Action<SwaggerUIOptions>? SwaggerUIConfigure;
        public Action<SecuritySettings>? SecurityConfig = null;
        public Func<GatewaySettings>? ApiGatewayConfigure = null;
        public Action<CronjobSettings>? CronjobConfigure = null;
        public Action<FileWacherSettings>? WatcherConfigure = null;

        internal static void AddCronjobSetting(CronjobSetting item)
        {
            if (JTechSetting.onAddCronjob != null)
                JTechSetting.onAddCronjob.Invoke(new AddCronjobEventArgs(item));
        }
        internal static void AddFileWatcherSetting(FileWacherSetting item)
        {
            if (JTechSetting.onAddFileWatcherSetting != null)
                JTechSetting.onAddFileWatcherSetting.Invoke(new AddWatcherSettingEventArgs(item));
        }

        internal static void RaiseSystemEvent(SystemEvents.SystemEventContact e)
        {
            JTechSetting.onSystemChange?.Invoke(e);
        }
    }

  
   
}
