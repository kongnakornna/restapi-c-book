using Jtech.Common.BusinessLogic.AutoNumber;
using Jtech.Common.DataStore;
using Jtech.Common.DataStore.Interface;
using Jtech.Common.DataStore.Paging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Drawing.Printing;
using static MassTransit.ValidationResultExtensions;

namespace Jtech.Common.BusinessLogic
{
    public enum Action
    {
        Create,
        Update,
        Delete
    }

    public class CRUDLogic :BusinessLogicBase 
    {
        private bool isInject = false;
        private Store? _store;
        public CRUDLogic(IServiceProvider provider) :base(provider)
        {
            this._store = provider.GetService<Store<CRUDLogic>>();
            isInject = this.Store != null;
        }

        private void StampDBLog<TModel>(TModel model,bool isNew)
        {
            var stampLogic = this.Services.GetService<DBLogLogic>();
            stampLogic!.StampLog(model, isNew);

        }

        private void AutoNumber<TModel>(TModel model)
        {
            var logic = this.Services.GetService<IAutoNumber>();
            if (logic != null)
            {
                var lstProp=typeof(TModel)
                .GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(AutoNumberAttribute), true).Count() == 1)
                .Select(p => new { propInfo = p, prefix = ((AutoNumberAttribute)p.GetCustomAttributes(typeof(AutoNumberAttribute), true)[0]).Prefix })
                .ToList();

                foreach (var prop in lstProp)
                    prop.propInfo.SetValue( model, logic.GetAutoNumber(prop.prefix));
            }
        }

        public Store? Store
        {
            get
            {
                return this._store;
            }
            set
            {
                //Allow Change for not inject only
                if (!isInject)
                    this._store = value;
            }
        }

        protected virtual LogicValidateException? Validate<TModel>(TModel model,Action action) where TModel : class
        {
            return null;
        }

        public virtual async Task<TModel> Create<TModel>(TModel model) where TModel : class
        {
            var excp = this.Validate<TModel>(model, Action.Create);
            if (excp != null)
                throw excp;
 
            if (model is IEntity)
                ((IEntity)model).Id = null;

            this.StampDBLog<TModel>(model,true);

            this.AutoNumber<TModel>(model);

            await this.Store!.Create< TModel>(model);

            await this.Services.RaiseSystemEvent<TModel>(model, EventType.Created);

            return await Task.FromResult(model);
        }

        public virtual async Task<TModel> Update<TModel>(TModel model) where TModel : class
        {
            var excp = this.Validate<TModel>(model, Action.Update);
            if (excp != null)
                throw excp;

            this.StampDBLog<TModel>(model, false);
            await this.Store!.Update(model);
            await this.Services.RaiseSystemEvent<TModel>(model, EventType.Updated);
            
            return await Task.FromResult(model);
        }

        public async Task DeleteById<TModel>(string id) where TModel : class
        {
            var excp = this.Validate<string>(id, Action.Delete);
            if (excp != null)
                throw excp;

            var model =await this.GetById<TModel>(id);

            await this.Store!.DeleteById<TModel>(id);

            await this.Services.RaiseSystemEvent<TModel>(model, EventType.Deleted);
        }

        public virtual async Task<TModel?> GetById<TModel>(string id) where TModel : class
        {
            var model = await this.Store!.GetById<TModel>(id);
            if (model == null)
                return await Task.FromResult(default(TModel));


            return await Task.FromResult(model);
        }

        public virtual async Task<IEnumerable<TModel>> GetList< TModel>(string? filter, string? orderBy = null) where TModel:class
        {

            return await Task.FromResult(this.Store!.GetByCondition<TModel>(filter, orderBy, null).Result);
        }
        public virtual async Task<IEnumerable<TModel>> Search<TModel>(Pagination page,string? filter,string? orderBy=null) where TModel:class
        {
            return await this.Store!.GetPagingByCondition<TModel>(page,filter, orderBy, null);
        }
    }
}
