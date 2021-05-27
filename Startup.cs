using EkompletGRPCExample.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using EkompletGRPCExample.Services;
using Microsoft.Extensions.Logging;

namespace EkompletGRPCExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            // Adds the database.
            services.AddDbContextPool<DatabaseContext>(builder =>
                {
                    var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") ??
                                           Configuration.GetConnectionString("DefaultConnection");
                    builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext context,
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            if (context.Database.EnsureCreated())
            {
                logger.LogInformation("Filling Database with Data");
                var populated = DbInitializer.Initialize(context, logger);
                if (!populated) logger.LogError("Error when filling database up!");
            }


            // Match the requests to an endpoint.
            app.UseRouting();


            // This call adds the services to the endpoint, which makes it possible to call them. 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<SalesmanService>();

                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Forkert adresse kald"); });
            });
        }
    }
}