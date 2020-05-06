using Microsoft.EntityFrameworkCore;
using Yugen.Toolkit.Standard.Data.Sample.Models;

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
}
