using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using Yugen.Toolkit.Standard.Data.Extensions;
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
        // this connection string must be retrieved from a secure file.
        private static readonly string _connectionString = "Data Source=MyDatabase.db";

        private static void ConfigureServices(IServiceCollection isc)
        {
            isc.AddDbContext<BloggingContext>(options => options.UseSqlite(_connectionString));
            //isc.AddSingleton<TheApp>();
        }

        private static IServiceProvider CreateServiceProvider()
        {
            // create service collection
            IServiceCollection isc = new ServiceCollection();
            ConfigureServices(isc);

            // create service provider
            return isc.BuildServiceProvider();
        }

        private static void Main(string[] args)
        {
            using (var scope = CreateServiceProvider().CreateScope())
            {
                //scope.ServiceProvider.GetService<TheApp>().Run();
            }
        }

        private class Factory : IDesignTimeDbContextFactory<BloggingContext>
        {
            public BloggingContext CreateDbContext(string[] args)
                => CreateServiceProvider().CreateScope().ServiceProvider.GetService<BloggingContext>();
        }
    }
}
