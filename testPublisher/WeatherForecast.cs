using Jtech.Common.Brokers.Base;
using MassTransit;

namespace testPublisher
{
    public class PublisherEndpoint2 : PublisherContext
    {
        public PublisherEndpoint2(PublisherProviderBase publisherProvider) : base(publisherProvider)
        {
        }
    }
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
