using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Brokers.Base
{
    public abstract class ConsumerProviderBase<TContact> : IConsumerProvider<TContact> where TContact : class
    {
        public abstract Task StartConsume(string Name, CancellationToken state);
    }
}
