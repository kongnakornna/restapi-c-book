using Helpers = Jtech.Common.Helpers;
using Jtech.Common.ApiGateway;
using MassTransit;
using System.Reflection;
using Jtech.Common;

Jtech.Common.Extensions.UseJTechRestApi(
    args: args,
    Configure: config =>
    {
        config.ApiGatewayConfigure = () =>
        {
            var json = System.IO.File.ReadAllText("./reverse.json");
            return Helpers.Json.DeserializeObject<GatewaySettings>(json);
        };


        config.BuilderConfigre = (builder => {

            builder.Services.AddMasstransitWithRabbit((context, configure) => {
                configure.Host("localhost", 5672, "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                configure.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(false));
            });
        });
    }
);