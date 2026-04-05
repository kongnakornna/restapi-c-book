using Jtech.Common.Brokers.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Jtech.Common.Brokers
{
    public class ConsumerHost<TContact> : IHostedService where TContact : class
    {
        private readonly IServiceProvider provider;
        private readonly IConsumerProvider<TContact> _consumerProvider;
        private string name;
        public ConsumerHost(IServiceProvider provider, string Name)
        {
            this.provider = provider;
            this.name = Name;
            this._consumerProvider = provider.GetRequiredService<IConsumerProvider<TContact>>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumerProvider.StartConsume(name, cancellationToken);
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
