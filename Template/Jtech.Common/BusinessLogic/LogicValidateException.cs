using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.BusinessLogic
{
    public class LogicValidateException : Exception
    {
        public LogicValidateException(string message,int errorCode) : base(message) {
            this.ErrorCode = errorCode;
        }

        public int ErrorCode { get; set; }
    }
}
