using Amazon.Runtime.Internal.Util;
using Jtech.Common.DataStore;
using Jtech.Common.Helpers;
using Jtech.Common.HttpClients.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.BusinessLogic
{
    public abstract class BusinessLogicBase
    {
        private readonly JTechServices _services;
        private readonly IServiceProvider _provider;

        public BusinessLogicBase(IServiceProvider provider)
        {
            _provider = provider;
            _services = new JTechServices(provider);
        }

        protected JTechServices Services
        {
            get
            {
                return this._services;
            }
        }

        public LineNotifyClient LineClient
        {
            get
            {
                return this.Services.LineClient;
            }
        }

        public JtechSMTPClient EmailNotify
        {
            get
            {
                return this.Services.SmtpClient;
            }
        }

        public async Task RaiseSystemEvent<T>(T Message,EventType e) where T:class
        {
            await this.Services.RaiseSystemEvent<T>(Message, e);
        }

        public IConfiguration Configuration
        {
            get 
            {
                return this.Services.Conguration;
            }
        }
    }
}
