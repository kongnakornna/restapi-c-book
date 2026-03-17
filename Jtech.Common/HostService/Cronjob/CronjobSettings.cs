using Jtech.Common.Settings;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HostService.Cronjob
{
   
    internal delegate void AddCronjobEventHandler(AddCronjobEventArgs e);

    internal class AddCronjobEventArgs : EventArgs
    {
        public AddCronjobEventArgs(CronjobSetting item)
        {
            Item = item;
        }
        public CronjobSetting Item { get; set; }
    }

    public class CronjobSettings : Collection<CronjobSetting> { }

    public class CronjobSetting
    {
        private string _cronExpr;
        private Type _type;


        public CronjobSetting(string CronExpr, ICronjobExecute service)
        {
            this.CreateJob(CronExpr, service.GetType());
        }


        public void CreateJob(string CronExpr, ICronjobExecute service)
        {
            this.CreateJob(CronExpr, service.GetType());
        }
        public void CreateJob(string CronExpr, string serviceType)
        {

            this.CreateJob(CronExpr, Type.GetType(serviceType));
        }

        public void CreateJob(string CronExpr, Type serviceType)
        {
            this._cronExpr = CronExpr;
            this._type = serviceType;
            JTechSetting.AddCronjobSetting(this);
        }
       
        public ICronjobExecute CreateService()
        {
            return (ICronjobExecute)Activator.CreateInstance(this._type); 
        }

        public string CronExpr 
        {
            get 
            {
                return _cronExpr;
            } 
        }
        internal DateTime NextOccurrence { get; set; }
    }
}
