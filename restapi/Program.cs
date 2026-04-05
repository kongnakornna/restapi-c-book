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
using TestCommon.Stores;          // ✅ ใช้ PostgreSqlStore
using Helpers = Jtech.Common.Helpers;
using Jtech.Common.BusinessLogic.Query;
using Microsoft.EntityFrameworkCore;   // ✅ เพิ่ม EF Core

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
            option.JwtKey = "Na@#1234";
            option.RequirePayload = true;
        };
        config.BuilderConfigre = options =>
        {
            // ==================== PostgreSQL Setup ====================
            var connectionString = config.Configuration.GetConnectionString("PostgreSql");
            options.Services.AddDbContext<BlogContext>(dbOpt =>
            {
                dbOpt.UseNpgsql(connectionString);
            });

            // ใช้ PostgreSQL Store ที่ implement ด้วย EF Core แทน MongoClient
            options.Services.AddScoped(typeof(Store<>), typeof(PostgreSqlStore<>));

            // CRUDLogic จะใช้ Store<> ที่เราลงทะเบียนไว้
            options.Services.AddLogic<CRUDLogic>();

            // SystemLogic ยังใช้ BlogContext ได้ตามเดิม (ตอนนี้เป็น PostgreSQL)
            options.Services.AddSystemLogic<BlogContext>();
            // ==========================================================

            //    Configuration for use htt client policy [retry, add log]
            options.Services.AddHttpClientWithPolicy<TestCommon.HttpClient.Ch3Client>(c =>
            {
                c.BaseAddress = "https://api-ch3plus.mello.me/api/configuration";
            });

            //     Configuration for use Line Notifycation Service
            options.Services.UseLineNotify();

            //     Configuration for use Email Notifycation Service
            //options.Services.UseEmail(option =>
            //{
            //    option.Password = "xxx";
            //});

            // Business Logic อื่น ๆ
            options.Services.AddLogic<TestInject>();

            // (Optional) Kafka / RabbitMQ / Ldap / MassTransit ตามเดิม (comment ไว้)
            //options.Services.AddKafkaPublisher<StorePublisherContext>(() =>
            //{
            //    return new ProducerConfig { BootstrapServers = "localhost", ClientId = Dns.GetHostName() };
            //}, "Book");

            //options.Services.AddRabbitPublisher<Blog>(new List<string> { "localhost" }, connection => {
            //    connection.UserName = "default";
            //    connection.Password = "password";
            //},"Blog");

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