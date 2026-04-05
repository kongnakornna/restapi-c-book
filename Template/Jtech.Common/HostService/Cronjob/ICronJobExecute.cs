using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HostService.Cronjob
{
    public interface ICronjobExecute
    {
        public Task ExecuteAsync(DateTime ExecuteTime);
    }
}
