using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HttpClients
{
    public class HttpClientSettings
    {
        public string BaseAddress { get; set; } = "";
        public int? RetryCountPolicy { get; set; }
        public string? ProxyUrl { get; set; }
    }
}
