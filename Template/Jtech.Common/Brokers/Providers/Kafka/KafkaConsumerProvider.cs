using Confluent.Kafka;
using Jtech.Common.Brokers.Base;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers = Jtech.Common.Helpers;

namespace Jtech.Common.Brokers.Providers.Kafka
{
    public class RabbitConsumerProvider<TContact> : ConsumerProviderBase<TContact> where TContact : class
    {
        private readonly ConsumerConfig consumeConfig;
        private readonly IServiceProvider provider;
        private readonly ILogger<RabbitConsumerProvider<TContact>>? logger;

        public RabbitConsumerProvider(IServiceProvider provider, Action<ConsumerConfig>? optionAction=null)
        {
            this.provider = provider;
            this.logger = provider.GetService<ILogger<RabbitConsumerProvider<TContact>>>();

            consumeConfig = new ConsumerConfig
            {
                GroupId = "GroupId",
                BootstrapServers = "localhost", 
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            if (optionAction != null)
                optionAction.Invoke(consumeConfig);
        }
        public override async Task StartConsume(string Name, CancellationToken state)
        {
            //using (var Consumer = new ConsumerBuilder<Ignore, string>(consumeConfig).Build())
            //{
            //    Consumer.Subscribe(Name);
            //    var brokerConsume = provider.GetRequiredService<IConsumerBroker<TContact>>();
            //    while (state.IsCancellationRequested)
            //    {
            //        try
            //        {
            //            var consumeResult = Consumer.Consume(state);
            //            var jsonMsg = consumeResult.Message.Value;

            //            brokerConsume.Consume(Helpers.Json.DeserializeObject<TContact>(jsonMsg));
            //        }
            //        catch (Exception ex)
            //        {
            //            logger.LogError(ex, ex.Message);
            //        }
            //    }
            //}
            //await Task.CompletedTask;
        }
    }
}
