using Jtech.Common.Settings;
using Jtech.Common.SystemEvents;
using MassTransit;
using Helpers = Jtech.Common.Helpers;

namespace Jtech.Common.SystemEvents
{
    public class SystemEventConsumer : IConsumer<SystemEventContact>
    {
        public async Task Consume(ConsumeContext<SystemEventContact> context)
        {
            JTechSetting.RaiseSystemEvent (context.Message);
        }
    }
}
