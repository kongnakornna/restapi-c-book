using Jtech.Common.HostService.Cronjob;
using Jtech.Common.HostService.FileWatcher;
using MassTransit.JobService;

namespace TestCommon.Background
{
    public class JobTest : ICronjobExecute
    {
        public async Task ExecuteAsync(DateTime ExecuteTime)
        {
            await Task.CompletedTask;
        }
    }
    public class JobWahcher : IWatcherExecute
    {
        public Task ExecuteAsync(string FullPath, WatcherChangeTypes CahngeType)
        {
            return Task.CompletedTask;
        }
    }
}
