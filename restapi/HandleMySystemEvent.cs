
using Jtech.Common;
using Jtech.Common.Settings;
using Jtech.Common.SystemEvents;
using Microsoft.AspNetCore.Components;
using TestCommon.Models;
using Helpers = Jtech.Common.Helpers;

namespace TestCommon.Background
{
    
    public class HandleMySystemEvent : Jtech.Common.Base.HandleSystemEventBase
    {
        public HandleMySystemEvent(IServiceProvider provider) : base(provider)
        {
        }

        protected override Task HandleEvent<T>(T Item, string Event) 
        {
            return Task.CompletedTask;
        }
    }
}
