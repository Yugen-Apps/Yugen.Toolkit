using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
/// Select: Startup Project: Yugen.Toolkit.Web.Sample
/// Go To: Package Manager Console
/// Select: Default Project: Yugen.Toolkit.Standard.Data.Sample
/// (Optional) Write: Remove-Migration
/// Write: Add-Migration {MigrationName}
/// </summary>
namespace Yugen.Toolkit.Web.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BloggingContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SQLite")))
                    .AddUnitOfWork<BloggingContext>();

            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddSingleton<IBlogRepositoryService, BlogRepositoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var blogService = app.ApplicationServices.GetService<IBlogRepositoryService>();

                    blogService.Add(new Blog { Url = "aaa" });
                    await context.Response.WriteAsync("added");
                    await context.Response.WriteAsync(System.Environment.NewLine);

                    var list = blogService.Get();
                    await context.Response.WriteAsync($"list: {list.Count}");
                    await context.Response.WriteAsync(System.Environment.NewLine);

                    if (list.Count > 0)
                    {
                        await context.Response.WriteAsync($"item: {list[0].Url}");
                    }
                });
            });
        }
    }
}
