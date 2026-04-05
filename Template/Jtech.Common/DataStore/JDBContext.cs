using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.DataStore
{
    public class JDBContext : DbContext
    {
        //private readonly IServiceProvider _provider;
        internal static List<Type> DbEntities = new List<Type>();

        //protected JDBContext(IServiceProvider provider, DbContextOptions options) : base(options)
        //{
        //    this._provider = provider;
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (Type t in JDBContext.DbEntities)
            {
                var entity = modelBuilder.Entity(t);

                var keyFields = t.GetMembers()
                        .Where(x => x.GetCustomAttribute<KeyAttribute>() != null)
                        .Select(x => x.Name)
                        .ToArray<string>();

                if (keyFields.Count() > 1)
                    entity.HasKey(keyFields);
            }
        }
    }
}
