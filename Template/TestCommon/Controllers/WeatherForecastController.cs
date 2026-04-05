using Microsoft.AspNetCore.Mvc;
using Helper = Jtech.Common.Helpers;
using System.Runtime.CompilerServices;
using Jtech.Common.Helpers;
using System.Text.Json;
using TestCommon.HttpClient;
using Jtech.Common.DataStore;
using Jtech.Common.DataStore.Interface;
using TestCommon.Models;
using Jtech.Common;
using Microsoft.EntityFrameworkCore;
using TestCommon.Database;
using MongoDB.Driver;
using Jtech.Common.HttpClients.Clients;
using Jtech.Common.Brokers.Base;
using Novell.Directory.Ldap;
using Jtech.Common.Ldap;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using Polly;
using Jtech.Common.Base;
using Newtonsoft.Json.Linq;
using TestCommon.Broker;
using System.Net.Mail;
using MassTransit.Configuration;
using System.Net;
using Jtech.Common.Settings;
using Jtech.Common.HostService.Cronjob;
using TestCommon.Background;
using Jtech.Common.HostService.FileWatcher;
using MassTransit;
using Microsoft.Win32;
using Jtech.Common.SystemEvents;
using Helpers = Jtech.Common.Helpers;

namespace TestCommon.Controllers
{
    public class WeatherForecastController : JtechControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private Ch3Client _ch3Client;

        private readonly LineNotifyClient _lineClient;

        private readonly Store<MongoClient> _store;
        private readonly Store<TMongoClient> _store2;

        private readonly Store<BlogContext> _blog;
        private readonly StorePublisherContext _pubBook;

        private readonly IPublishEndpoint _publisher;

        public WeatherForecastController(IServiceProvider provider) : base(provider)
        {
            _ch3Client = this.Services.GetService<Ch3Client>();
            // _repo = repo;
            _lineClient = Services.GetService<LineNotifyClient>();
            _blog = Services.GetService<Store<BlogContext>>();
            _store = Services.GetService<Store<MongoClient>>();
            _store2 = Services.GetService<Store<TMongoClient>>();
            _pubBook = Services.GetService<StorePublisherContext>();
            _publisher = Services.GetService<IPublishEndpoint>();

          
        }

        [Authorize]
        [HttpGet(Name = "GetWeatherForecast")]
        [ResponseCache(CacheProfileName = "Default")]
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

        [HttpGet("GetGMT")]
        public DateTime GetGMT()
        {
            return DateTime.Now.GetGMTNow();
        }

        [HttpGet("TestSeriailize")]
        public string TestJson()
        {
            string e;
           
            return Helper.Json.Serialize(new { 
                id="test",
                name="test name"
            });
        }

        [HttpPut("TestSeriailize")]
        public IActionResult PutJson([FromBody] JsonDocument obj)
        {


            return this.Ok(obj);
        }

        [HttpGet("GetSchedule")]
        public JsonDocument GetSchedule([FromQuery] string? schDate= "20231201")
        {
            
            return this._ch3Client.GetSch(schDate).Result;
        }

        [HttpGet("GetMD5")]
        public string GetMD5([FromQuery] string? data = null)
        {
            return data.ToMD5();
        }

        [HttpPost("createBookMongo")]
        public async Task<Book> createBookMongo([FromBody] Book book)
        {

            await _store.Create(book);
            await _store2.Create(book);
           
            _lineClient.Notify("Save Success", "rFJysTONxwGwSE0TNeUB4u37Br5DQ8NkcWoLyn5jByG");
            return book;
        }

        [HttpPut("updateBookMongo")]
        public async Task<Book> UpdateBookMongo([FromBody] Book book)
        {
            await _store.Update(book);
            _lineClient.Notify("Update Success", "rFJysTONxwGwSE0TNeUB4u37Br5DQ8NkcWoLyn5jByG");
            return book;
        }
        [HttpPost("migrateDB")]
        public void MigrateDB()
        {
            using (var db = new BlogContext())
            {
                db.Database.EnsureCreated();
                db.Database.Migrate();
            }
        }

        [HttpPost("createBlog")]
        public async Task<Blog> createBlog([FromBody] Blog blog)
        {
            await _blog.Create(blog);
            _lineClient.Notify("Save Success", "rFJysTONxwGwSE0TNeUB4u37Br5DQ8NkcWoLyn5jByG");
            return blog;
        }

        [HttpPost("Upload")]
        public  IActionResult Upload(List<IFormFile> files)
        {
            //StringValues value = "";
            //Request.Headers.TryGetValue("payload", out value);
            return Ok();
        }

        [HttpPost("email")]
   
        public void SendMail()
        {
            var config = Services.Conguration.GetValue<string>("name");

            Services.SmtpClient.SendEmail("test subject", "test", "jarun@ctc-g.co.th");
        }

        [HttpPost("addCronjob")]
        public void AddCronjob()
        {
            var item = new CronjobSetting("*/1 * * * *", new JobTest());
         
        }

        [HttpPost("addWatcher")]
        public void AddFileWacher()
        {
            var item = new FileWacherSetting("*/1 * * * *", new JobWahcher());
        }

        [HttpPost("PublishMassTransit")]
        public void PublishMassTransit()
        {
            this.Services.RaiseSystemEvent<Book>(new Book { BookName="TESTT"},EventType.Created).GetAwaiter();

           
            
        }
    }
}
