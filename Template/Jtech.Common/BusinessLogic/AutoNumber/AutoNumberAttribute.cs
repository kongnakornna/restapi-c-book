using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.BusinessLogic.AutoNumber
{
    public class AutoNumberAttribute:Attribute
    {
        public string Prefix { get; set; }
        public AutoNumberAttribute(string prefix)
        {
            this.Prefix = prefix;
        }
    }
}
