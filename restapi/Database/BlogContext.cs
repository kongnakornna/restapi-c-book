using Jtech.Common.BusinessLogic.AutoNumber;
using Jtech.Common.BusinessLogic.Query;
using Microsoft.EntityFrameworkCore;
using RestCommon.Models;

namespace RestCommon.Database
{
    // ถ้าไม่จำเป็นต้องใช้ MongoDB สามารถลบคลาส TMongoClient ทิ้งได้
    // public class TMongoClient : MongoClient { ... }

    public class BlogContext : DbContext
    {
        // Constructor รับ options จาก Program.cs (ใช้ DI)
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        // DbSet ทั้งหมดตามโครงสร้างเดิม
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<NumberFormat> NumberFormat { get; set; }
        public DbSet<Query> Query { get; set; }
        public DbSet<Connections> Connection { get; set; }

        // ไม่ต้อง override OnConfiguring เพราะ Connection String ถูกส่งจากภายนอก
        // และไม่ใช้ SQLite หรือ MongoDB อีกต่อไป

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // กำหนดเพิ่มเติมเกี่ยวกับ Entity (Primary Key, Index, ความสัมพันธ์) ได้ที่นี่
        }
    }
}