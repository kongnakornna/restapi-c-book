using Jtech.Common.DataStore;
using Jtech.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace Jtech.Common.BusinessLogic.AutoNumber
{
    public class DefaultAutoNumberLogic : BusinessLogicBase, IAutoNumber
    {
        private readonly Store? _store;
        private const int TOTAL_LENGTH = 15;

        public DefaultAutoNumberLogic(IServiceProvider provider) : base(provider)
        {
            this._store = this.Services.GetService<Store<DefaultAutoNumberLogic>>();
        }

        public string GetAutoNumber(string prefix)
        {
            var dt = DateTime.Now.GetGMTNow();
            var yy = dt.Year;
            var mm = dt.Month;
            var isUpdate = true;

            NumberFormat item =this._store.GetByCondition<NumberFormat>(x => x.Year == yy && x.Month == mm && x.Prefix == prefix)
                .Result.FirstOrDefault();

            if (item == null)
            {
                //New month
                item = new NumberFormat { Year=yy,Month=mm,Prefix=prefix,Current=0};
                isUpdate = false;
            }


            var autoNumber= $"{prefix}" +
                    $"{yy.ToString()}" +
                    $"{mm.ToString().PadLeft(2,'0')}" +
                    $"{(item.Current + 1).ToString().PadLeft(TOTAL_LENGTH-(prefix.Length+6),'0')}";

            item.Current += 1;
            if(isUpdate)
                this._store.Update<NumberFormat>(item).GetAwaiter();
            else
                this._store.Create<NumberFormat>(item).GetAwaiter();
            return autoNumber;
        }
    }
}
