using Employees.Core.BusinessLogic;
using Employees.Core.EF;
using Employees.Core.EF.Repository;
using Employees.Core.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using System.IO;

namespace Employees.WebAPI
{
    public class Startup
    {
        public readonly string _applicationName = 
            Assembly.GetEntryAssembly().GetName().Name;
        
        public readonly string _applicationVersion =
            $"v{ Assembly.GetEntryAssembly().GetName().Version.Major}." +
            $"{Assembly.GetEntryAssembly().GetName().Version.Minor}." +
            $"{Assembly.GetEntryAssembly().GetName().Version.Build}";
       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Swashbuckle Swagger
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
            services.AddTransient<IEmployeeRepository, EmployeeRepositoryEF>();

            // It also recycles open connection in a connected ADO.NET style!
            services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("employees"));
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
