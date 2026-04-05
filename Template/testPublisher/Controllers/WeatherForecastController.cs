using Confluent.Kafka;
using Jtech.Common.SystemEvents;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Mvc;
using Helpers = Jtech.Common.Helpers;

namespace testPublisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IPublishEndpoint _pub;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPublishEndpoint pub)
        {
            _logger = logger;
            _pub = pub;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("Publish")]
        public async Task<IActionResult> Publisher()
        {

            await this._pub.Publish<SystemEventContact>(new SystemEventContact { Value="test"});
            return  Ok();
        }

        [HttpGet("configuration")]
        public async Task<IActionResult> GetConfig()
        {
            var cfg=System.IO.File.ReadAllText(@".\appsettings.json");
            return Ok(cfg);
        }

        [HttpPost("PublishMassTransit")]
        public void PublishMassTransit()
        {
            _pub.Publish<SystemEventContact>(new SystemEventContact
            {
                Event = "Add"

            }) ;
        }
    }
}
