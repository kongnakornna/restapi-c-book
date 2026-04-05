using System;
using System.Threading.Tasks;

namespace Jtech.Common.Brokers.Base
{
    public interface IConsumerProvider<TContact> where TContact : class
    {
        Task StartConsume(string Name, CancellationToken state);
    }
}
