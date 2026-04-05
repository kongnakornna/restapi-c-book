using MassTransit;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers = Jtech.Common.Helpers;

namespace Jtech.Common.SystemEvents
{
    internal delegate void SystemEventHandler(SystemEventContact e);
    public class SystemEventContact : EventArgs
    {

        public string Event { get; init; }
        public string From { get; init; }

        public object Value { get; set; }
       
        public string ValueType { get; set; }
    
    }
}
