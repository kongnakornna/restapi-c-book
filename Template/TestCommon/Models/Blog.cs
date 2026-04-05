using Jtech.Common;
using Jtech.Common.DataStore.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCommon.Models
{
    [Table("Blog")]
    public class Blog:IEntity
    {
        
        [Required]
        public string BlogId { get; set; }

        [Column("Url")]
        public string Url { get; set; }

        public List<Post> Posts { get; } = new();

        [Column("Id")]
        public string Id { get; set; } = "";

    }

    [Table("Post")]
    public class Post
    {
        [Column("PostId")]
        public int PostId { get; set; }

        [Column("Title")]
        public string Title { get; set; }
        [Column("Content")]
        public string Content { get; set; }
        [Column("BlogId")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

}
