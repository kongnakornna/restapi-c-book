using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.BusinessLogic.AutoNumber
{
    public interface IAutoNumber
    {
        public string GetAutoNumber(string prefix);
    }
}
