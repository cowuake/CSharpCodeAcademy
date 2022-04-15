using CustomerOrderManagement.Core.BusinessLogic;
using CustomerOrderManagement.Core.EF.DataContext;
using CustomerOrderManagement.Core.EF.Repositories;
using CustomerOrderManagement.Core.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CustomerOrderManagement.Services.WebAPI
{
    public class Startup
    {
        // Automatically retrieve the application name
        public readonly string _applicationName =
            Assembly.GetEntryAssembly().GetName().Name;

        // Automatically retrieve the application version
        public readonly string _applicationVersion =
            $"v{ Assembly.GetEntryAssembly().GetName().Version.Major}." +
            $"{Assembly.GetEntryAssembly().GetName().Version.Minor}." +
            $"{Assembly.GetEntryAssembly().GetName().Version.Build}";

        // No need to modify original from template
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // No need to modify original from template
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Swashbuckle Swagger | Quite standard, can be easily migrated
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = _applicationName,
                    Version = _applicationVersion
                });

                string fileName = $"{typeof(Startup).Assembly.GetName().Name}.xml";

                c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName));
            });

            // Dependency Injection
            services.AddTransient<IMainBusinessLogic, MainBusinessLogic>();
            services.AddTransient<ICustomerRepository, EFCoreCustomerRepository>();
            services.AddTransient<IOrderRepository, EFCoreOrderRepository>();

            // It also recycles open connection in a connected ADO.NET style!
            services.AddDbContext<CustomerOrderManagementContext>(options =>
            {
                // Pay attention to identifier from connection string (read from JSON conf)
                options.UseSqlServer(Configuration.GetConnectionString("customer_order_management"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Swashbuckle Swagger
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    "v1/swagger.json",
                    ""
                   );
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}