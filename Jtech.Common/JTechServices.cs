using Confluent.Kafka;
using Jtech.Common.Base;
using Jtech.Common.HttpClients.Clients;
using Jtech.Common.Settings;
using Jtech.Common.SystemEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common
{
    public enum EventType
    { 
        Created,
        Updated,
        Deleted
    }

    public class JTechServices
    {
        private readonly IServiceProvider _provider;

        public JTechServices(IServiceProvider provider)
        {
            _provider = provider;
        }

        public object? GetService(Type serviceType)
        {
            return _provider.GetService(serviceType);
        }

        public T? GetService<T>()
        {
            return _provider.GetService<T>();
        }

        public ILogger<JtechControllerBase>? Logger
        {
            get
            { 
                return GetService<ILogger<JtechControllerBase>>(); 
            }
        }

        public LineNotifyClient LineClient 
        { 
            get 
            { 
                var client= this.GetService<LineNotifyClient>();
                if (client == null)
                    throw new NullReferenceException($"Null reference {nameof(LineNotifyClient)}.You can use Services.UseLineNotify() extension in main program.");
                return client;
            } 
        }

        public JtechSMTPClient SmtpClient 
        {
            get
            {
                var client = this.GetService<JtechSMTPClient>();
                if (client == null)
                    throw new NullReferenceException($"Null reference {nameof(JtechSMTPClient)}.You can use Services.UseEmail() extension in main program.");
                return client;
            } 
        }

        public IConfiguration Conguration
        {
            get 
            {
                return this.GetService<IConfiguration>();
            }
        }

        public IPublishEndpoint? MassTransitPublisher
        {
            get 
            {
                return this.GetService<IPublishEndpoint>();
            }
        }

        public async Task MassTransitPublish<T>(T message) where T:class
        {
            this.MassTransitPublisher?.Publish(message);
            await Task.CompletedTask;
        }

        public async Task RaiseSystemEvent<T>(T Value, EventType Event) where T : class
        {
            this.MassTransitPublisher?.Publish<SystemEventContact>(new SystemEventContact {
                Event = Event.ToString(),
                Value = Value,
                ValueType =Value.GetType().FullName
            });
            await Task.CompletedTask;
        }
    }
}
