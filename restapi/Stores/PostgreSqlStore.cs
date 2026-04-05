using Microsoft.EntityFrameworkCore;
using restapi.Models;   // เปลี่ยน namespace

namespace restapi.Database
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }
        public DbSet<Blog> Blogs { get; set; }
    }
}