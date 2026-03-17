using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Base
{
    public class SecuritySettings
    {
        public ParameterLocation In { get; set; } = ParameterLocation.Header;
        public SecuritySchemeType Type { get; set; } = SecuritySchemeType.Http;

        public string BearerFormat { get; set; } = "JWT";
        public string Scheme { get; set; } = "Bearer";

        public int Token_Expire_Minutes{ get; set; } = 120;
        public string JwtIssurer { get; set; } = "localhost.com";
        public string JwtKey { get; set; } = "YourSecretKeyForAuthenticationOfApplication";

        public bool RequirePayload { get; set; } = true;
    }
}
