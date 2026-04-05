using Confluent.Kafka;
using Jtech.Common.Brokers.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers = Jtech.Common.Helpers;

namespace Jtech.Common.Brokers.Providers.Rabbit
{
    public class RabbitWorkerConsumerProvider<TContact> : ConsumerProviderBase<TContact> where TContact : class
    {
        private IConnection _connection;
        private readonly IServiceProvider provider;

        public RabbitWorkerConsumerProvider(IServiceProvider provider, ConnectionFactory factory, List<string> hostList)
        {
            _connection = factory.CreateConnection(hostList);
            this.provider = provider;
        }
        public override async Task StartConsume(string Name, CancellationToken state)
        {
            var brokerConsume = provider.GetRequiredService<IConsumerBroker<TContact>>();

            IModel channel = this._connection.CreateModel();

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                string message = Encoding.UTF8.GetString(e.Body.ToArray());
                var contact = Helpers.Json.DeserializeObject<TContact>(message);
                brokerConsume.Consume(contact);
            };

            channel.QueueDeclare(Name, false, false, false, null);
            channel.BasicConsume(queue: Name, autoAck: true, consumer: consumer);

            await Task.CompletedTask;
        }
    }
}
