using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using Yugen.Toolkit.Standard.Data.Sample;

/// <summary>
/// Add a reference to Yugen.Toolkit.Standard.Data.Sample
///
/// How To Create a Migration
/// Select: Startup Project: Yugen.Toolkit.Core.Sample
/// Go To: Package Manager Console
/// Select: Default Project: Yugen.Toolkit.Standard.Data.Sample
/// (Optional) Write: Remove-Migration
/// Write: Add-Migration {MigrationName}
/// </summary>
namespace Yugen.Toolkit.Core.Sample
{
    class Program
    {
        /// <summary>
        /// Option 1: simple and fast approach just to generate migrations
        /// </summary>
        private static void Main(string[] args)
        {
            new BloggingContextFactory().CreateDbContext(null);
        }

        public class BloggingContextFactory : IDesignTimeDbContextFactory<BloggingContext>
        {
            public BloggingContext CreateDbContext(string[] args) => 
                new BloggingContext(new DbContextOptionsBuilder<BloggingContext>()
                    .UseSqlite("Data Source=blog.db").Options);
        }


        /// <summary>
        /// Option 2: Dependency Injection
        /// </summary>
        //private static void Main(string[] args)
        //{
        //    var serviceProvider = CreateServiceProvider();
        //    // serviceProvider.GetService<BloggingContext>().Database.EnsureCreated();
        //}

        //private static IServiceProvider CreateServiceProvider()
        //{
        //    // create service collection
        //    var serviceCollection = new ServiceCollection();
        //    ConfigureServices(serviceCollection);

        //    // create service provider
        //    return serviceCollection.BuildServiceProvider();
        //}

        //private static void ConfigureServices(IServiceCollection serviceCollection)
        //{
        //    serviceCollection.AddDbContext<BloggingContext>(options => 
        //        options.UseSqlite("Data Source=MyDatabase.db"));
        //}

        //private class Factory : IDesignTimeDbContextFactory<BloggingContext>
        //{
        //    public BloggingContext CreateDbContext(string[] args)
        //        => CreateServiceProvider().CreateScope().ServiceProvider.GetService<BloggingContext>();
        //}
    }
}
