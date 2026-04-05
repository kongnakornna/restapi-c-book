using Jtech.Common.Settings;
using Jtech.Common.SystemEvents;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Base
{
    public abstract class HandleSystemEventBase : BackgroundService
    {
        private IServiceProvider _provider;
        private JTechServices _service;
        public HandleSystemEventBase(IServiceProvider provider)
        {
            _provider = provider;
            _service = new JTechServices(provider);
            JTechSetting.onSystemChange += JTechSetting_onSystemChange;
        }

        protected JTechServices Services
        {
            get
            {
                return _service;
            }
        }

        protected abstract Task HandleEvent<T>(T Item, string Event);


        private void JTechSetting_onSystemChange(SystemEventContact e)
        {
            var t = Type.GetType(e.ValueType);
            var obj = Helpers.Json.DeserializeObject(
                    Helpers.Json.Serialize(e.Value), t);
            HandleEvent(obj, e.Event);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}