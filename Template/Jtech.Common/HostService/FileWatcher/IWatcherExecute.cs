using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HostService.FileWatcher
{
    public interface IWatcherExecute
    {
        public Task ExecuteAsync(string FullPath,WatcherChangeTypes CahngeType);
    }

}
