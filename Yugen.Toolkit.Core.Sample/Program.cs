using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using Yugen.Toolkit.Standard.Data.Extensions;
using Yugen.Toolkit.Standard.Data.Sample;
using Yugen.Toolkit.Standard.Data.Sample.Interfaces;
using Yugen.Toolkit.Standard.Data.Sample.Models;
using Yugen.Toolkit.Standard.Data.Sample.Repository;
using Yugen.Toolkit.Standard.Data.Sample.Services;

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
        //private static void Main(string[] args)
        //{
        //    new BloggingContextFactory().CreateDbContext(null);
        //}

        //public class BloggingContextFactory : IDesignTimeDbContextFactory<BloggingContext>
        //{
        //    public BloggingContext CreateDbContext(string[] args) => 
        //        new BloggingContext(new DbContextOptionsBuilder<BloggingContext>()
        //            .UseSqlite("Data Source=YugenCore.sqlite").Options);
        //}


        /// <summary>
        /// Option 2: Dependency Injection
        /// </summary>
        private static void Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();

            var isCreated = serviceProvider.GetService<BloggingContext>().Database.EnsureCreated();
            Console.WriteLine($"isCreated: {isCreated}");

            var blogService = serviceProvider.GetService<IBlogRepositoryService>();

            blogService.Add(new Blog { Url = "aaa" });
            Console.WriteLine("added");

            var list = blogService.Get();
            Console.WriteLine($"list: {list.Count}");

            if (list.Count > 0)
            {
                Console.WriteLine($"item: {list[0].Url}");
            }
        }

        private static IServiceProvider CreateServiceProvider()
        {
            return new ServiceCollection()
                .AddDbContext<BloggingContext>(options => options.UseSqlite("Data Source=YugenCore.sqlite"))
                    .AddUnitOfWork<BloggingContext>()
                .AddTransient<IBlogRepository, BlogRepository>()
                .AddSingleton<IBlogRepositoryService, BlogRepositoryService>()
                .BuildServiceProvider();
        }

        private class Factory : IDesignTimeDbContextFactory<BloggingContext>
        {
            public BloggingContext CreateDbContext(string[] args)
                => CreateServiceProvider().CreateScope().ServiceProvider.GetService<BloggingContext>();
        }
    }
}

//https://devblogs.microsoft.com/premier-developer/demystifying-the-new-net-core-3-worker-service/
//https://stackoverflow.com/questions/58183920/how-to-setup-app-settings-in-a-net-core-3-worker-service
//https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1