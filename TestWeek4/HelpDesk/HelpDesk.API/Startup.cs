using HelpDesk.Core.ArchitecturalUtilities;
using HelpDesk.Core.Interface;
using HelpDesk.Core.Mock.DataRepository;
using Microsoft.OpenApi.Models;

namespace HelpDesk.API
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
            services.AddControllers();

	        // MANUALLY ADDED
            DependencyInjector.Register<ITicketRepository, MockTicketRepository>();

            // MANUALLY ADDED
            //services.AddSwaggerGenNewtonsoftSupport(); // We are sooooo sorry :'(

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Help Desk (new gen support system with self-evident name)",
                    Version = "1.0"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Better to put Swagger is if the solution has to be released
            }

            // MANUALLY ADDED
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Help Desk 1.0");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
