using AmazonBookStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonBookStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; set; }

        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BooksDBConnection"]);
            });

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();

            services.AddRazorPages();

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //cooreesponds to the wwwroot
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            // the endpoint labels and tabbing is not neccessary but done for organizations and clean coding
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name : "categorypage",
                    pattern : "{bookCategory}/pg.{pageNum}",
                    defaults : new { Controller = "Home", action = "Index" });


                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "pg.{pageNum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute(
                    "type",
                    "{bookCategory}",
                    new { Controller = "Home", Action = "Index", pageNum = 1});




                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });
        }
    }
}
