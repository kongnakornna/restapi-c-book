using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Brokers.Base
{
    public abstract class PublisherProviderBase
    {
        private string GetQName<T>(string? QName)
        {
            return QName==null?typeof(T).Name:QName;
        }
        protected abstract Task PublishToBrokerAsync<TContact>(TContact Message, string QName ) where TContact : class;
        public virtual async Task Publish<TContact>(TContact contact, string? QName = null) where TContact : class
        {
            await this.PublishToBrokerAsync<TContact>(contact, this.GetQName<TContact>(QName));
        }
    }
}
