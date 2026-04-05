using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.MiddleWare
{
    public class PayloadInfoBase
    {
        public DateTime create_date { get; set; }
        public string user_name { get; set; } = "";
    }
}
