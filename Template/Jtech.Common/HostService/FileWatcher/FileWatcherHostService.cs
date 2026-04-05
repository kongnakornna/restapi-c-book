using Amazon.Runtime.Internal.Util;
using Amazon.SecurityToken.Model.Internal.MarshallTransformations;
using Jtech.Common.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HostService.FileWatcher
{
    public class FileWatcherHostService : IHostedService, IDisposable
    {
        private readonly IList<FileSystemWatcher> _watchers;
        private readonly ILogger<FileWatcherHostService> _logger;
        private readonly FileWacherSettings _settings;
        private readonly string _path;

        public FileWatcherHostService(ILogger<FileWatcherHostService> logger, FileWacherSettings settings)
        {
            _watchers=new List<FileSystemWatcher>();
            _logger = logger;
            _settings = settings;
            JTechSetting.onAddFileWatcherSetting += JTechSetting_onAddFileWatcherSetting;
        }

        private void JTechSetting_onAddFileWatcherSetting(AddWatcherSettingEventArgs e)
        {
            this._settings.Add(e.Item);
            this._watchers.Add(GetWatcher(e.Item.WatchPath, e.Item.Filter));
        }

        private FileSystemWatcher GetWatcher(string path, string? filter)
        {
            var w = string.IsNullOrEmpty(filter) ? new FileSystemWatcher(path) : new FileSystemWatcher(path, filter);
            w.EnableRaisingEvents = true;
            w.Changed += W_Changed;
            return w;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var item in _settings)
                this._watchers.Add(GetWatcher(item.WatchPath, item.Filter));

            return Task.CompletedTask;
        }

        private  void W_Changed(object sender, FileSystemEventArgs e)
        {
           var item= _settings.Where(x => e.FullPath.Contains(x.WatchPath)).FirstOrDefault();
            if (item != null) 
                item.CreateService()!.ExecuteAsync(e.FullPath,e.ChangeType);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            if (_watchers != null)
                foreach (var w in _watchers)
                    w.Dispose();
        }
    }
}