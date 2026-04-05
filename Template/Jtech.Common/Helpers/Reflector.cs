using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Jtech.Common.Brokers.Base;
using Jtech.Common.Brokers.Providers.Kafka;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Jtech.Common.Helpers
{
    public static class Reflector
    {
        public static T CreateInstanst<T>(IServiceProvider provider)
        {
            return provider.GetRequiredService<T>();
        }
        public static T? CreateInstanst<T>()
        {
            return Activator.CreateInstance<T>();
        }
        public static T? CreateInstanst<T>(params object[] ConstructorArgs)
        {
            return (T)Activator.CreateInstance(typeof(T), ConstructorArgs);
        }

        public static Assembly? EntryAssembly
        {
            get
            {
                return Assembly.GetEntryAssembly();
            }
        }

        public static object? InvokeGeneric<TRef>(string MethodeName, Type typeOfGeneric, object RefObj = null, object[] parameters = null)
        {
            MethodInfo? method = typeof(TRef).GetMethod(MethodeName);
            if (method == null)
                return null;
            
            MethodInfo generic = method.MakeGenericMethod(typeOfGeneric);
            return generic.Invoke(RefObj, parameters);
        }        
    }
}
