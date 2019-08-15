using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using DakarRally.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using DakarRally.Api.Middleware;
using System.Threading;
using System;
using Microsoft.Extensions.Options;
using DakarRally.Core.Contracts;
using DakarRally.Core.Services;
using DakarRally.Core.Options;

namespace DakarRally.Api
{
    public class Startup
    {
        private Timer _timer;
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DakarRallyContext>(opt => opt.UseInMemoryDatabase("DakarRace"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRaceRepository, RaceRepository>();
            services.AddScoped<IVehicleRepository, VehiclesRepository>();
            services.AddScoped<Simulator>();
            services.AddScoped<StatsGenerator>();

            services.Configure<RallyOptions>(Configuration.GetSection("Rally"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceScopeFactory services, IOptions<RallyOptions> config)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();

            // Using Timer for simplicity. Usually would use something more resilient, like Hangfire, Quartz, Azure Function...
            int updateRate = config.Value.UpdateRate;
            _timer = new Timer(Run, services, TimeSpan.Zero,
                TimeSpan.FromSeconds(updateRate));
        }

        private void Run(object state)
        {
            IServiceScopeFactory services = (IServiceScopeFactory)state;
            using (var scope = services.CreateScope())
            {
                Simulator _raceSimulator = scope.ServiceProvider.GetRequiredService<Simulator>();
                _raceSimulator.Update();
            }
        }
    }
}