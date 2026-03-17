using Confluent.Kafka;
using Esprima.Ast;
using Jtech.Common.Brokers.Base;
using Jtech.Common.Brokers.Providers.Kafka;
using Jtech.Common.Brokers.Providers.Rabbit;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RabbitMQ.Client;
using Helpers = Jtech.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Jtech.Common.Brokers
{
    public static class Extension
    {
        public static IServiceCollection AddKafkaPublisher<TPublisherContext>(this IServiceCollection services, Func<ProducerConfig> optionAction, string? name = null) where TPublisherContext : PublisherContext
        {
            services.AddTransient<TPublisherContext>(services =>
            {
                var config = optionAction.Invoke();
                config.ClientId = string.IsNullOrEmpty(config.ClientId) ? Dns.GetHostName() : config.ClientId;

                return Helpers.Reflector.CreateInstanst<TPublisherContext>(
                    new object[] { new KafkaPublisherProvider(config) }
                );
            });
            return services;
        }
        public static IServiceCollection AddRabbitWorkerPublisher<TPublisherContext>(this IServiceCollection services, List<string> hosts  , Action<ConnectionFactory>? optionAction=null, string? QueueName = null) where TPublisherContext : class
        {
           services.AddTransient<TPublisherContext>(services =>
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.Port = 5672;
                if(optionAction!=null)
                    optionAction.Invoke(factory);

                return Helpers.Reflector.CreateInstanst<TPublisherContext>(new object[] {
                    new RabbitWorkerPublisherProvider( factory, hosts)
                });
                
            });
            return services;
        }

    }
}
