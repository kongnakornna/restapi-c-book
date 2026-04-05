using Jtech.Common.Base;
using Jtech.Common.HttpClients.Clients;
using Jtech.Common.HttpClients;
using Jtech.Common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;
using Jtech.Common.ApiGateway;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Jtech.Common.MiddleWare;
using Jtech.Common.HostService;
using Microsoft.Extensions.Logging;
using MassTransit;
using System.Reflection;
using Jtech.Common.Formatter;
using Microsoft.AspNetCore.Mvc.Formatters;
using Jtech.Common.BusinessLogic.AutoNumber;
using Jtech.Common.BusinessLogic.Identity;
using Jtech.Common.BusinessLogic.Query;
using Jtech.Common.BusinessLogic;
using Jtech.Common.DataStore;
using Microsoft.EntityFrameworkCore;

namespace Jtech.Common
{
    public static class Extensions
    {
     
        public static void UseJTechRestApi(Action<JTechSetting> Configure, params string[] args)
        {
            var setting= new JTechSetting();
            Configure.Invoke(setting);
           
            //log Setting
            var basePath = AppContext.BaseDirectory;
            var logConfig = new LoggerConfiguration()
              .WriteTo.Console()
              .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug)
                  .WriteTo.File(Path.Combine(basePath, "Logs", "Debug", "debug_.log"), rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true))
              .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                  .WriteTo.File(Path.Combine(basePath, "Logs", "information", "info_.log"), rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true))
              .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                  .WriteTo.File(Path.Combine(basePath, "Logs", "error", "error_.log"), rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true));

            var builder = WebApplication.CreateBuilder(args);
            //for external configure
            if(setting.BuilderConfigre !=null)
                setting.BuilderConfigre.Invoke(builder);

            if (setting.UrlServiceConfiguration != null)
                builder.Services.AddHttpClientWithPolicy<AppConfigurationClient>(client => {
                    client.BaseAddress = setting.UrlServiceConfiguration;
                });
            if (setting.ApiGatewayConfigure !=null)
                builder.Services.AddHttpClientWithPolicy<ApiGateway.ApiGateway>(client => {});

            //config log
            builder.Host.UseSerilog();
            if (setting.LogConfigure != null)
                setting.LogConfigure.Invoke(logConfig);
            Log.Logger = logConfig.CreateLogger();

            //config security
            SecuritySettings? securityConfig = null;
            if (setting.SecurityConfig != null)
            {
                securityConfig = new SecuritySettings();
                setting.SecurityConfig.Invoke(securityConfig);
            }
            builder.Services.AddResponseCaching();

            // Add services to the container.
            builder.Services.AddControllers(options => {
                options.CacheProfiles.Add("Default",
                                              new CacheProfile()
                                              {
                                                  Duration = 10
                                              });
                options.RespectBrowserAcceptHeader = true;
            });

            //builder.Services.AddControllers(options => { 
            //    options.SuppressAsyncSuffixInActionNames = false; 
            //}).AddNewtonsoftJson();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor();

            if (setting.EnableAcceptCsvFormatter || setting.EnableAcceptXmlFormatter)
            {
                var mvc = builder.Services.AddMvc(
                   options => {
                       if (setting.EnableAcceptCsvFormatter)
                           options.OutputFormatters.Add(new CsvFormatter());

                       if (setting.EnableAcceptXmlFormatter)
                           options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                   }
               );
               if (setting.EnableAcceptXmlFormatter)
                    mvc.AddXmlDataContractSerializerFormatters();

            }

            if (securityConfig != null)
            {
                builder.Services.AddSingleton<IAuthorizationHandler, PayloadHandler>();
                builder.Services.AddSingleton<SecuritySettings>(securityConfig);
                setting.SwaggerConfigure = options =>
                {
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        In = securityConfig.In,
                        Type = securityConfig.Type,
                        BearerFormat = securityConfig.BearerFormat,
                        Scheme = securityConfig.Scheme
                    });   
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id=securityConfig.Scheme
                                }
                            },
                            new string[]{}
                        }
                    });

                    if (securityConfig.RequirePayload)
                    {
                        options.AddSecurityDefinition("Payload", new OpenApiSecurityScheme
                        {
                            Description = "Please enter a payload",
                            Name = "payload",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "string"
                        });
                        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type=ReferenceType.SecurityScheme,
                                        Id="Payload"
                                    },
                                },
                                new string[]{}
                            }
                        });
                    }
                };
            }

            builder.Services.AddSwaggerGen(setting.SwaggerConfigure);

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            if (securityConfig != null)
            {
                builder.Services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(typeof(AuthenBaseController).Assembly));

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = securityConfig.JwtIssurer,
                        ValidAudience = securityConfig.JwtIssurer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityConfig.JwtKey)),
                    };
                });
            }
                
            if (setting.CronjobConfigure != null)
                builder.Services.AddCronJob(setting.CronjobConfigure);
            if(setting.WatcherConfigure !=null)
                builder.Services.AddFileWatcher(setting.WatcherConfigure);

            //   builder.Services.AddMassTransitWithRabbit(setting.MassTransitConfigure);

            AppConfigureAndRun(builder, setting, securityConfig);
        }

        public static IConfigurationBuilder AddApiConfiguration(this IConfigurationBuilder builder, IServiceCollection services, Action<HttpClientSettings> configClient)
        {
            try
            {
                services.AddHttpClientWithPolicy<AppConfigurationClient>(configClient);
                var provider = services.BuildServiceProvider();
                var client = provider.GetService<AppConfigurationClient>();

                builder.AddJsonStream(client.GetJsonConfiguration().Result.Content.ReadAsStream());
            }
            catch { }

            return builder;
        }


        public static IServiceCollection UseAcceptFomatter(this IServiceCollection services)
        {
            services.AddMvc(
                 options => {

                     options.OutputFormatters.Add(new XmlSerializerOutputFormatter());

                     options.OutputFormatters.Add(new CsvFormatter());
                 }
             ).AddXmlDataContractSerializerFormatters();

            return services;
        }

        internal static void AppConfigureAndRun(WebApplicationBuilder builder, JTechSetting setting,SecuritySettings? securityConfig)
        {
            var app = builder.Build();

            //JTechCommon.Services = builder.Services;
            //JTechCommon.Configuration = new ConfigurationHelper((IConfiguration)builder.Configuration);
            //JTechCommon.Logger = Log.Logger;
            //JTechCommon.ServiceProvider = app.Services;

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // Configure the Api Gateway
            if (setting.ApiGatewayConfigure != null)
                app.UseApiGW(setting.ApiGatewayConfigure);

            app.UseSwagger();

            if (setting.EnableSwaggerUI)
                app.UseSwaggerUI(setting.SwaggerUIConfigure);

            app.UseResponseCaching();

            app.UseHttpsRedirection();

            if (securityConfig != null)
            {
                app.UseAuthentication();

                app.UseAuthorization();

            }
            app.UseCors(x => {
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                x.AllowAnyHeader();
                x.WithExposedHeaders("payload");
            });
            app.UseHttpsRedirection();

            app.MapControllers();

            if (setting.AppConfigure != null)
                setting.AppConfigure.Invoke(app);

            app.Run();
        }

        public static IServiceCollection AddSystemLogic<T>(this IServiceCollection services) where T:DbContext
        {
            services.AddScoped<IIdentity, IdentityFromPayload>();
            services.AddLogic<DBLogLogic>();

            services.AddLogic<DefaultAutoNumberLogic, T>();
            services.AddLogic<QueryLogic, T>();

            services.AddScoped<IAutoNumber, DefaultAutoNumberLogic>();
            return services;
        }

        public static IServiceCollection AddHandleSystemEvent<THandle>(this IServiceCollection services) where THandle: HandleSystemEventBase
        {
            return services.AddHostedService<THandle>();
        }
        public static IServiceCollection AddMasstransitWithRabbit(this IServiceCollection services,Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator> actConfig)
        {
            services.AddMassTransit(x =>
            {
               //Add consumers with entry assembly
                x.AddConsumers(Assembly.GetEntryAssembly());
                x.AddConsumers(Assembly.GetAssembly(typeof(JTechServices)));

                x.UsingRabbitMq((context, configure) =>
                {
                    actConfig.Invoke(context, configure);
                });

            });
            return services;
        }
    }
}

