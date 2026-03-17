using Jtech.Common.BusinessLogic.AutoNumber;
using Jtech.Common.BusinessLogic.Query;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using TestCommon.Models;

namespace TestCommon.Database
{
    public class TMongoClient : MongoClient
    {
        public TMongoClient(string connectionString) : base(connectionString)
        {
        }
    }
    public class BlogContext : DbContext
    {
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Post { get; set; }

        public DbSet<NumberFormat> NumberFormat{get;set;}

        public DbSet<Query> Query { get; set; }

        public DbSet<Connections> Connection { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ".\\Database\\demo.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
            //this.Set<>

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Blog>();
            //modelBuilder.Entity()
            base.OnModelCreating(modelBuilder);
        }
    }
}