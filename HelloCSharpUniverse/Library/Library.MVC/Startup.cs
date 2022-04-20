using Library.Core.BusinessLogic;
using Library.Core.EFCore;
using Library.Core.EFCore.Repository;
using Library.Core.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddScoped<IMainBusinessLogic, MainBusinessLogic>();

            // Data repositories
            services.AddScoped<IBookRepository, EFCoreBookRepository>();
            services.AddScoped<IBookGenreRepository, EFCoreBookGenreRepository>();
            services.AddScoped<IUserRepository, EFCoreUserRepository>();

            services.AddDbContext<LibraryContext>(c =>
            {
                c.UseSqlServer(Configuration.GetConnectionString("library"));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", p => p.RequireRole("Administrator"));
                options.AddPolicy("User", p => p.RequireRole("User"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Home}/{action=Index}/{id?}");
                    pattern: "{controller=Library}/{action=Index}/{id?}");
            });
        }
    }
}