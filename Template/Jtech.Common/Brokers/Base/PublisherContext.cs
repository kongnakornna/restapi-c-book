using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Brokers.Base
{
    public class PublisherContext
    {
        internal PublisherProviderBase _publisherProvider;
        public PublisherContext(PublisherProviderBase publisherProvider)
        {
            this._publisherProvider = publisherProvider;
        }

        public async Task Publish<TContact>(TContact Message) where TContact : class
        {
            await this._publisherProvider.Publish<TContact>(Message);
        }
    }
}
