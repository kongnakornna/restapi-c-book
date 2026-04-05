using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Ldap
{
    public class LdapProvider
    {
        private readonly Settings setting;

        public LdapProvider(Settings Options)
        {
            setting = Options;
        }

        public bool Authentication(string userName, string password)
        {
            using (var cn = new LdapConnection())
            {
                try
                {
                    cn.Connect(setting.Host, setting.Port);
                    cn.Bind(setting.BIND_DN, setting.AdminPwd);
                    ILdapSearchResults result = cn.Search(setting.BASE_DN, LdapConnection.ScopeSub, "(sAMAccountName=" + userName + ")", null, false);
                    //Add delay for wait result from ldap
                    Thread.Sleep(100);

                    if (result.Count <= 0)
                        return false;

                    cn.Bind(result.ElementAt(0).Dn, password);
                    LdapWhoAmIResponse resp = cn.WhoAmI();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cn.Disconnect();
                }
            }
        }
    }
}
