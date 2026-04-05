using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.BusinessLogic.Identity
{
    public class IdentityForAttribute : Attribute
    {
        private bool _forCreate;
        public IdentityForAttribute(bool ForCreate = false)
        {
            _forCreate = ForCreate;
        }

        public bool ForCreate
        {
            get
            {
                return _forCreate;
            }
        }
    }
}
