using Esprima.Ast;
using Jtech.Common.Base;
using Jtech.Common.DataStore;
using Jtech.Common.Ldap;
using Jtech.Common.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TestCommon.Database;
using TestCommon.Models;

namespace TestCommon.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : AuthenBaseController
    {
        private readonly LdapProvider _auth;

        public LoginController(SecuritySettings options
                                //,LdapProvider auth
                                ) : base(options)
        {
            //_auth=auth;
        }

        protected override bool Login(string username, string password)
        {
            //return this._auth.Authentication(username, password);
            return username == "admin";
        }
        protected override PayloadInfoBase? GetPayloadInfo(LoginRequest loginInfo)
        {
            MyPayload payload = new MyPayload();

            payload.user_name = loginInfo.UserName;
            payload.create_date = DateTime.Now;
            payload.User_Id = "U000012";
            payload.Avatar = @"http://sdasdf.jpg";
            payload.Auth = new string[] { "add_order", "addd" };
            return payload;
        }

        public class MyPayload : PayloadInfoBase
        {
            public string User_Id { get; set; }
            public string Avatar { get; set; }
            public string[] Auth { get; set; }
        }
    }


}
