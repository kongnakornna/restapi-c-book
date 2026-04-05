using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Ldap
{
    public class Settings
    {
        public string Host { get; set; } = "10.202.3.200";
        public int Port { get; set; } = 389;
        public string AdminPwd { get; set; } = "bM=7QSB2";
        public string BIND_DN { get; set; } = "CN=LDAP STM,OU=Services,OU=Users,OU=TrueDigital,DC=truedigital,DC=group";
        public string BASE_DN { get; set; } = "DC=truedigital,DC=group";
    }
}
