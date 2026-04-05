using Jtech.Common.DataStore.Interface;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using MassTransit;

namespace Jtech.Common.DataStore
{
    public static class Extensions
    {
        public static string DBEscape(this IEntity entity, string Text)
        {
            return (string.IsNullOrEmpty(Text)) ? Text : Text.Replace("'", "''");
        }

        internal static List<T> AutoMap<T>(this IDataReader reader) 
        {
            var entities = new List<T>();
            T obj = default(T);

            while (reader.Read())
            {
                List<PropertyInfo> props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        .Where(p => Attribute.IsDefined(p, typeof(ColumnAttribute))
                                        )
                                        .Select(p => p).ToList();

                if (props == null)
                {
                    //map with same value
                    obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        if (!object.Equals(reader[prop.Name], DBNull.Value))
                            prop.SetValue(obj, reader[prop.Name], null);
                    entities.Add(obj);
                }
                else
                {
                    //map with column attribute
                    obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in props)
                    {
                        ColumnAttribute columnInfo = prop.GetCustomAttribute<ColumnAttribute>();
                        if (!object.Equals(reader[columnInfo.Name], DBNull.Value))
                            prop.SetValue(obj, reader[columnInfo.Name], null);
                    }
                    entities.Add(obj);
                }
            }
            reader.Close();

            return entities;
        }

        public static IServiceCollection AddStore<TClient>(this IServiceCollection services, string DatabaseName) where TClient: IMongoClient
        {
            return services.AddScoped<Store>(provider => {
                var client = provider.GetRequiredService<TClient>();
                return new Store(client.GetDatabase(DatabaseName));
            });
        }

        public static IServiceCollection AddStore<TDBContext>(this IServiceCollection services) where TDBContext : DbContext
        {
            return services.AddScoped<Store>(provider => {
                var db = provider.GetRequiredService<TDBContext>();
                return new Store(db);
            });
        }

        public static IServiceCollection AddLogic<TLogic>(this IServiceCollection services) where TLogic : class
        {
            return services.AddScoped<TLogic>();
        }

        public static IServiceCollection AddLogic<TLogic, TClient>(this IServiceCollection services, string DatabaseName) where TClient : IMongoClient where TLogic : class
        {
            services.AddScoped<Store<TLogic>>(provider => {
                var client = provider.GetRequiredService<TClient>();
                return new Store<TLogic>(client.GetDatabase(DatabaseName));
            });
            return AddLogic<TLogic>(services);
        }

        public static IServiceCollection AddLogic<TLogic, TDBContext>(this IServiceCollection services) where TDBContext : DbContext where TLogic : class
        {
            services.AddScoped<Store<TLogic>>(provider => {
                var db = provider.GetRequiredService<TDBContext>();
                return new Store<TLogic>(db);
            });
            return AddLogic<TLogic>(services);
        }

        public static IServiceCollection AddMongoClient<T>(this IServiceCollection services, Func<MongoClient> actMongo) where T : MongoClient
        {
            try
            {
                BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
                BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
                BsonSerializer.RegisterSerializer(new ObjectIdSerializer(BsonType.String));
            }
            catch { }

            return services.AddSingleton<T>(provider =>
            {
                return (T)Convert.ChangeType(actMongo.Invoke(), typeof(T));
            });
        }
    }
}
