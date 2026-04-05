using Confluent.Kafka;
using Jtech.Common.Brokers.Base;
using Helpers = Jtech.Common.Helpers;

namespace Jtech.Common.Brokers.Providers.Kafka
{
    public class KafkaPublisherProvider : PublisherProviderBase
    {
        private readonly ProducerConfig produceConfig;
        public KafkaPublisherProvider(ProducerConfig setting)
        {
            this.produceConfig = setting;
        }

        protected override async Task PublishToBrokerAsync<TContact>(TContact Message, string QName) where TContact : class
        {
            using (var producer = new ProducerBuilder<Null, string>(produceConfig).Build())
                await producer.ProduceAsync(QName, new Message<Null, string> { Value = Helpers.Json.Serialize(Message) });
        }
    }
}
