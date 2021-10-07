using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using Titinski.WebAPI.Interfaces.Repositories.ImageMetadataRepository;
using Titinski.WebAPI.Interfaces.Storage;
using Titinski.WebAPI.Interfaces.UnitOfWork;

namespace Titinski.WebAPI
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    var allowedHosts = Configuration.GetSection("AllowedHosts").Get<string[]>();
                    builder
                    .SetIsOriginAllowed(origin => {
                        return allowedHosts.Contains(origin);
                        })
                    .AllowAnyMethod();
                });
            });
            services.AddControllers();
            services.AddSwaggerGen();
            // handlers
            services.AddScoped<Handlers.IMainHandler, Handlers.MainHandler>();
            // services
            services.AddScoped<IImageMetadataRepository, EFCore.Repositories.SqlRepo>();
            services.AddScoped<IImageStorage, Services.ImageStorage.FtpStorage>();
            services.AddScoped<IUnitOfWork, EFCore.UnitOfWork>();
            // configurations
            services.Configure<AppSettings.FtpConfig>(Configuration.GetSection("App:Ftp"));
            ConfigureEFCore(services);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "The-Real-Titinski API V1");
            });
            
            if (!env.IsDevelopment())
            {
                // when working locally, causes silent exception
                // need to figure out what is the real condition to check here
                app.UseHttpsRedirection();
            }            

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureEFCore(IServiceCollection services)
        {
            var sqlConfig = Configuration.GetSection("App:Sql").Get<AppSettings.SqlConfig>();
            var connectionString = $"server={sqlConfig.Address};user={sqlConfig.Username};password={sqlConfig.Password};database={sqlConfig.DbName}";

            var serverVersion = new MariaDbServerVersion(new Version(10, 4));

            // Replace 'YourDbContext' with the name of your own DbContext derived class.
            services.AddDbContext<EFCore.ImagesDbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    .EnableSensitiveDataLogging() // <-- These two calls are optional but help
                    .EnableDetailedErrors()       // <-- with debugging (remove for production).
            );
        }
    }
}
