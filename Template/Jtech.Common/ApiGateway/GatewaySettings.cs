using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.ApiGateway
{
    public class GatewaySettings:Dictionary<string,ForwardItem>{ }

    public class ForwardItem
    {
        public string StartWithPath { get; set; } = "";
        public string ForwardPath { get; set; } = "";
    }
}
