using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

/// <summary>
/// Add a reference to Yuogen.Toolkit.Standard.Data
/// </summary>
namespace Yugen.Toolkit.Standard.Data.Sample
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }


        public BloggingContext() { }
        public BloggingContext(DbContextOptions<BloggingContext> options) : base(options) { }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new List<Post>();
    }
}
