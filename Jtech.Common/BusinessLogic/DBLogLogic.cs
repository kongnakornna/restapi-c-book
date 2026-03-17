using Jtech.Common.Helpers;
using Jtech.Common.BusinessLogic.Identity;
using Microsoft.OpenApi.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.BusinessLogic
{

    public class DBLogLogic
    {
        private readonly IIdentity _identity;
        public DBLogLogic(IIdentity identity)
        {
            this._identity = identity;
        }

        private Type GetNonNullableType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type; // avoid type becoming null
        }

        private PropertyInfo? GetPropertyByFieldName<T>(Type propertyType,bool isNew)
        {
            return typeof(T)
                .GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(IdentityForAttribute), true).Count() == 1
                    && ((IdentityForAttribute)p.GetCustomAttributes(typeof(IdentityForAttribute), true).FirstOrDefault()).ForCreate == isNew)
                .Where(prop => (GetNonNullableType(prop.PropertyType) == propertyType || prop.PropertyType == propertyType) && prop.CanWrite)
                .FirstOrDefault();
        }

        public void StampLog<T>(T model, bool isNew)
        {
            var propBy = this.GetPropertyByFieldName<T>(typeof(string), isNew);
            if(propBy !=null)
                propBy.SetValue(model, this._identity.GetUerName());

            var propDate = this.GetPropertyByFieldName<T>(typeof(DateTime), isNew);
            if (propDate != null)
                propDate.SetValue(model, DateTime.Now.GetGMTNow());
        }
    }
}
