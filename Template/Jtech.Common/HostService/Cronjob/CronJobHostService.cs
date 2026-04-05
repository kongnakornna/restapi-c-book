using Jtech.Common.Helpers;
using Jtech.Common.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HostService.Cronjob
{
    public class CronJobHostService : IHostedService, IDisposable
    {
        private readonly ILogger<CronJobHostService> _logger;
        private  Timer? _timer = null;
        private readonly CronjobSettings? _setting;

        public CronJobHostService(ILogger<CronJobHostService> logger, CronjobSettings? setting=null)
        {
            _logger = logger;
            _setting = setting;

            if (setting == null)
                _setting = new CronjobSettings();
            else
            {
                //Init next occurrnce 
                DateTime basetime = TruncateMillisecond(DateTime.Now.GetGMTNow());
                for(var i=_setting.Count-1;i>=0;i--)
                {
                    DateTime next;
                    if (TrySetNextOccurrence(basetime, _setting[i].CronExpr, out next))
                        _setting[i].NextOccurrence = next;
                    else
                        _setting.RemoveAt(i);
                }
            }
            JTechSetting.onAddCronjob += JTechSetting_onAddCronjob;
        }
        private void JTechSetting_onAddCronjob(AddCronjobEventArgs e)
        {
            DateTime next;
            if (TrySetNextOccurrence(TruncateMillisecond(DateTime.Now.GetGMTNow()), e.Item.CronExpr, out next))
            {
                e.Item.NextOccurrence = next;
                _setting.Add(e.Item);
            }
        }

        private bool TrySetNextOccurrence(DateTime basetime, string cronExpr, out DateTime next)
        {
            next = this.GetNextOccurrence(basetime, cronExpr);
            return next >= TruncateMillisecond(DateTime.Now.GetGMTNow());
        }

        private async void DoWork(object? state)
        {
            try
            {
                if (_setting.Count == 0)
                {
                    _logger.LogInformation("Timed Hosted Service not have schedule???");
                    return;
                }

                var currentTime = TruncateMillisecond(DateTime.Now.GetGMTNow());
                DateTime next;

                List<Task> lstTask = new List<Task>();
                foreach (var cron in _setting.Where(x => currentTime == x.NextOccurrence))
                {
                    var srv = cron.CreateService();
                    if (srv != null)
                    {
                        lstTask.Add(srv.ExecuteAsync(currentTime));
                        _logger.LogInformation($"Timed Hosted Service run job {srv.GetType().Name}.At:{currentTime.ToString("dd/MM/yyyy HH:mm:ss")}");
                    }

                    if (TrySetNextOccurrence(currentTime, cron.CronExpr, out next))
                        cron.NextOccurrence = next;
                    else
                        _setting.Remove(cron);
                }
                await Task.WhenAll(lstTask);
            }
            catch (Exception e)
            {
                _logger.LogError($"Timed Hosted Service Error :{e.Message}");
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Cronjob hosted service start at {DateTime.Now}");
            _logger.LogInformation($"Cronjob hosted service is ready!!!!!");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Cronjob hosted service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private DateTime GetNextOccurrence(DateTime baseTime, string cronExpression)
        {
            return TruncateMillisecond(NCrontab.CrontabSchedule.Parse(cronExpression).GetNextOccurrence(baseTime));
        }

        private DateTime TruncateMillisecond(DateTime dt)
        {

            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }
    }
}
