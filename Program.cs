
using DTOsTask.Repository;
using DTOsTask.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DTOsTask.Model;

namespace DTOsTask
{
    public class Program
    {
        // Constructor for Program class, takes IConfiguration as a parameter to access configuration settings.
        public Program(IConfiguration configuration)
        {
            Configuration = configuration; // Initializes the Configuration property with the provided configuration object.
        }

        // Property to hold the IConfiguration instance, which provides access to application configuration (e.g., appsettings.json, environment variables).
        public IConfiguration Configuration { get; }

        // Method to configure the services required by the application. This is part of the dependency injection setup.
        public void ConfigureServices(IServiceCollection services)
        {
            // Registers the ApplicationDbContext with the dependency injection container.
            // Uses a connection string from the app's configuration settings (e.g., appsettings.json or environment variables) to connect to the SQL Server database.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Registers the IProductRepository and IProductService interfaces with their respective implementations.
            // These are services that can be injected into controllers or other services.
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();

            // Adds MVC controller services to the DI container. This is needed for the application to handle HTTP requests and responses.
            services.AddControllers();
        }

        // Method to configure the HTTP request pipeline for the application.
        // This configures middleware that handles HTTP requests (e.g., routing, controllers).
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Adds routing middleware to the request pipeline. This enables the app to route incoming requests to the appropriate endpoints.
            app.UseRouting();

            // Configures the app to use endpoint routing, which maps controller actions to HTTP routes.
            app.UseEndpoints(endpoints =>
            {
                // Maps the controllers to the endpoint routing system. This tells the app to look for controller actions for handling HTTP requests.
                endpoints.MapControllers();
            });
        }

    }
}
