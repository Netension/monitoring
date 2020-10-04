using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus;
using Prometheus;

namespace Netension.Monitoring.Example.NetCoreWebApplication
{
    public class Startup
    {
        public static ILoggerFactory LoggerFactory { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPrometheusMetrics(LoggerFactory)
                .RegistrateCounter("example_counter_metric", "Example counter metric.");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ICounterManager counterManager)
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
                    counterManager.Increase("example_counter_metric");
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapMetrics();
            });
        }
    }
}
