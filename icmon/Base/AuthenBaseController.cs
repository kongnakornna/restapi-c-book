using Amazon.Runtime.Credentials.Internal;
using AuthenticationPlugin;
using Esprima;
using Jtech.Common.Helpers;
using Jtech.Common.MiddleWare;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Polly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Base
{
  
    [ApiController]
    [Route("[controller]")]
    public abstract class AuthenBaseController : ControllerBase
    {
       
        private readonly SecuritySettings settings;
        public AuthenBaseController(SecuritySettings options) {
            this.settings = options;
        }

        protected abstract bool Login(string username, string password);
       
        protected virtual PayloadInfoBase? GetPayloadInfo(LoginRequest loginInfo)
        {
            return new PayloadInfoBase
            {
                user_name=loginInfo.UserName,
                create_date= DateTime.Now.GetGMTNow(),
            };
        }

        [HttpPost("Signin")]
        public  IActionResult Signin([FromBody] LoginRequest loginInfo)
        {
            if (!this.Login(loginInfo.UserName, loginInfo.Password))
                return Unauthorized();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.settings.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var Sectoken = new JwtSecurityToken(this.settings.JwtIssurer,
                      this.settings.JwtIssurer,
                      new List<Claim> { new Claim("userName", loginInfo.UserName) },
                      expires:  DateTime.Now.AddMinutes(this.settings.Token_Expire_Minutes),
                      signingCredentials: credentials);

            //Payload info with header
            var payloadInfo = this.GetPayloadInfo(loginInfo);
            Response.Headers.AddPayload(payloadInfo, this.settings.JwtKey);
            
            return new ObjectResult(new AuthenResult
            {
                token_type = "jwt",
                valid_from = Sectoken.ValidFrom,
                valid_to = Sectoken.ValidTo,
                access_token = new JwtSecurityTokenHandler().WriteToken(Sectoken)
            });
        }
    }

    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthenResult
    {
        public string token_type { get; set; } = "JWT";
        public DateTime valid_from { get; set; }

        public DateTime valid_to { get; set; }

        public string access_token { get; set; } = string.Empty;
    }
}
