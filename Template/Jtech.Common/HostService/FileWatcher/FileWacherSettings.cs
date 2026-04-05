using Jtech.Common.HostService.Cronjob;
using Jtech.Common.Settings;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HostService.FileWatcher
{
    internal delegate void AddFileWatcherSettingEventHandler(AddWatcherSettingEventArgs e);

    internal class AddWatcherSettingEventArgs : EventArgs
    {
        public AddWatcherSettingEventArgs(FileWacherSetting item)
        {
            Item = item;
        }
        public FileWacherSetting Item { get; set; }
    }

    public class FileWacherSettings : Collection<FileWacherSetting> { }

    public class FileWacherSetting
    {
        public string _watchPath;
        private Type _type;
        private string? _filter;

        public FileWacherSetting(string watchPath, IWatcherExecute service,string? filter=null)
        {
            _watchPath = watchPath;
            _filter = filter;
            _type = service.GetType();
            JTechSetting.AddFileWatcherSetting(this);
        }
        public IWatcherExecute CreateService()
        {
            return (IWatcherExecute)Activator.CreateInstance(this._type);
        }
        public string WatchPath
        {
            get { return this._watchPath; }
        }
        public string? Filter
        {
            get
            {
                return _filter;
            }
        }
    }
}
