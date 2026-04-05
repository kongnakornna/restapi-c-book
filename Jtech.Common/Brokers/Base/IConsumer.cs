using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Brokers.Base
{
    public interface IConsumerBroker<TContact> where TContact : class
    {
        Task Consume(TContact? Message);
    }
}