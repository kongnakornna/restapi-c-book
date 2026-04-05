using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Helpers = Jtech.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Jtech.Common.DataStore.Interface;
using Jtech.Common.Helpers;
using System.Transactions;

namespace Jtech.Common.Base
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public abstract class JtechControllerBase : ControllerBase
    {
        private readonly IServiceProvider _provider;
        private readonly JTechServices _services;

        public JtechControllerBase(IServiceProvider provider)
        {
            _provider = provider;
            _services = new JTechServices(provider);
        }

        protected JTechServices Services 
        {
            get
            {
                return this._services;
            }
        }
        protected StringValues GetHeaderValue(string keyName)
        {
            Request.Headers.TryGetValue(keyName, out StringValues headerValue);
            HttpContext.Items[keyName] = headerValue;
            return headerValue;
        }
        protected T? GetHeaderPayload<T>() where T:class
        {
            Request.Headers.TryGetValue("payload", out StringValues headerValue);
            if (headerValue.Count > 0)
                return Helpers.Json.DeserializeObject<T>(headerValue[0]);
            else
                return default(T);

        }

        protected U CopyToObject<T, U>(T obj)
        {
            if (typeof(T) == typeof(U))
                return obj!.ChangeType<U>();

            var modelParams = typeof(U).GetConstructors()[0].GetParameters();
            object[] para = new object[modelParams.Length];

            var i = 0;
            foreach (var p in modelParams)
                if (obj.GetType().GetProperties().Where(x => x.Name == p.Name && x.PropertyType == p.ParameterType).Count() > 0)
                    para[i] = obj.GetType().GetProperty(p.Name).GetValue(obj);

            U model = (U)Activator.CreateInstance(typeof(U), para);
            if (model != null)
            {
                foreach (var p in obj.GetType().GetFields())
                    if (model.GetType().GetProperties().Where(x => x.Name == p.Name && x.PropertyType == p.FieldType).Count() > 0)
                        model.GetType().GetField(p.Name).SetValue(model, obj.GetType().GetProperty(p.Name).GetValue(obj));

                foreach (var p in obj.GetType().GetProperties())
                    if (model.GetType().GetProperties().Where(x => x.Name == p.Name && x.PropertyType == p.PropertyType).Count() > 0)
                        model.GetType().GetProperty(p.Name).SetValue(model, obj.GetType().GetProperty(p.Name).GetValue(obj));
            }
            return model;
        }

        protected void CopyObject<TCopyFrom, TCopyTo>(TCopyFrom from, TCopyTo to)
        {
            if (typeof(TCopyFrom) != typeof(TCopyTo) && from != null && to != null)
            {
                foreach (var p in from.GetType().GetFields())
                    if (to.GetType().GetProperties().Where(x => x.Name == p.Name && x.PropertyType == p.FieldType).Count() > 0)
                        to.GetType().GetField(p.Name).SetValue(to, from.GetType().GetProperty(p.Name).GetValue(from));

                foreach (var p in from.GetType().GetProperties())
                    if (to.GetType().GetProperties().Where(x => x.Name == p.Name && x.PropertyType == p.PropertyType).Count() > 0)
                        to.GetType().GetProperty(p.Name).SetValue(to, from.GetType().GetProperty(p.Name).GetValue(from));
            }
        }

        protected void TransactionScope(Action action)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    scope.Complete();
                }
            }
        }
    }
}
