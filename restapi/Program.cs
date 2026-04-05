using Amazon.Runtime.Internal.Util;
using Confluent.Kafka;
using Jtech.Common;
using Jtech.Common.ApiGateway;
using Jtech.Common.Brokers;
using Jtech.Common.BusinessLogic;
using Jtech.Common.BusinessLogic.AutoNumber;
using Jtech.Common.DataStore;
using Jtech.Common.Formatter;
using Jtech.Common.HostService.Cronjob;
using Jtech.Common.HostService.FileWatcher;
using Jtech.Common.HttpClients;
using Jtech.Common.HttpClients.Clients;
using Jtech.Common.BusinessLogic.Identity;
using Jtech.Common.Settings;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Polly;
using RabbitMQ.Client;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Text;
using TestCommon;
using TestCommon.Background;
using TestCommon.Broker;
using TestCommon.Controllers;
using TestCommon.Database;
using TestCommon.HttpClient;
using TestCommon.Models;
using Helpers = Jtech.Common.Helpers;
using Jtech.Common.BusinessLogic.Query;

Jtech.Common.Extensions.UseJTechRestApi(
    args: args,
    Configure: config => {
        //    Configuration for use appsetting from another api
        //config.UrlServiceConfiguration = "https://localhost:7256/WeatherForecast/configuration",
       
        //    Configuration for user reverse proxy api gateway
        //config.ApiGatewayConfigure = () =>
        //{
        //    var json = System.IO.File.ReadAllText("./reverse.json");
        //    return Helpers.Json.DeserializeObject<GatewaySettings>(json);
        //};

        //    Configuration for Swagger 
        config.SwaggerConfigure = options =>
        {
            //== Configuration for Swagger Doc
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Test Swagger Title",
                Version = "v1",
            });
        };
        //  Configure for Cronjob
        //config.CronjobConfigure = option =>
        //{
        //    option.Add(new CronjobSetting("*/1 * * * *", new JobTest()));
        //};
        //  Configure for FileSystemWatcher
        //config.WatcherConfigure = option =>
        //{
        //    option.Add(new FileWacherSetting("C:\\Users\\Jarun\\Documents\\projects", new JobWahcher()));
        //    option.Add(new FileWacherSetting("C:\\Users\\Jarun\\Documents\\scm", new JobWahcher()));
        //};
        config.SecurityConfig = option =>
        {
            option.JwtIssurer = "localhost.com";
            option.JwtKey = "YourSecretKeyForAuthenticationOfApplication";
            option.RequirePayload = true;
        };
        config.BuilderConfigre = options =>
        {

            //    Configuration for use htt client policy [retry, add log]
            options.Services.AddHttpClientWithPolicy<TestCommon.HttpClient.Ch3Client>(c =>
            {
                c.BaseAddress = "https://api-ch3plus.mello.me/api/configuration";
            });

            ////     Configuration for use Line Notifycation Service
            options.Services.UseLineNotify();

            //     Configuration for use Email Notifycation Service
            //options.Services.UseEmail(option =>
            //{
            //    option.Password = "xxx";
            //});

            //     Configuration for use SQLLite database 1
            options.Services.AddDbContext<BlogContext>().AddEntityFrameworkSqlite();

            //     Configuration for use MongoDB Database
            options.Services.AddMongoClient<MongoClient>(() => {
                return new MongoClient(@"mongodb://localhost");
            });

            options.Services.AddStore<MongoClient>("test");

            //      Configuration for BusinessLogic
           

            //==> Inject overrid store to crud logic
            options.Services.AddLogic<CRUDLogic, MongoClient>("test");


            options.Services.AddSystemLogic<BlogContext>();


            options.Services.AddLogic<TestInject>();


            //options.Services.AddKafkaPublisher<StorePublisherContext>(() =>
            //{
            //    return new ProducerConfig { BootstrapServers = "localhost", ClientId = Dns.GetHostName() };
            //}, "Book");

            //      Example for Configuration Rabbit Publisher Blog
            //options.Services.AddRabbitPublisher<Blog>(new List<string> { "localhost" }, connection => {
            //    connection.UserName = "default";
            //    connection.Password = "password";
            //},"Blog");

            //      Example for User Ldap Authentication 
            //options.Services.AddLdapProvider(op => {
            //    op.Host = "localhost";
            //});

            //options.Services.AddHandleSystemEvent<HandleMySystemEvent>();

            //options.Services.AddMasstransitWithRabbit((context, configure) => {
            //    configure.Host("localhost", 5672, "/", h =>
            //    {
            //        h.Username("guest");
            //        h.Password("guest");
            //    });
            //    configure.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(false));
            //});


            //options.Services.AddResolver();
            //options.Services.AddTestDi<ClientResolve>((provider) => {
            //    IWrite x = new WriteFile();
            //    x.message = "Write message form file";
            //    return x;
            //});
            //options.Services.AddTestDi<ClientResolve2>((provider) => {
            //    IWrite x = new WriteConsole();
            //    x.message = "Write message form console";
            //    return x;
            //});
        };
    }
);