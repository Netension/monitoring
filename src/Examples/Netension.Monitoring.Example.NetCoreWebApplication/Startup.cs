using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Netension.Monitoring.Prometheus;
using Prometheus;
using System.Collections.Generic;
using System.Reflection;

namespace Netension.Monitoring.Example.NetCoreWebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddOpenApiDocument(settings =>
            {
                var version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

                settings.DocumentName = $"v{version}";
                settings.Title = "Netension.Monitoring.Prometheus .NET Core Example";
                settings.Version = version;
            });

            services.AddPrometheusMetrics((registry, provider) =>
            {
                registry.RegistrateCounter(Metrics.CreateCounter("example_counter_metric", "Example counter metric."));
                registry.RegistrateCounter("example_counter_metric_without_label", "Example counter metric without label.");
                registry.RegistrateCounter("example_counter_metric_with_label", "Example counter metric with label.", new List<string> { "label1", "label2" });

                registry.RegistrateGauge(Metrics.CreateGauge("example_gauge_metric", "Example gauge metric."));
                registry.RegistrateGauge("example_gauge_metric_without_label", "Example gauge metric without label.");
                registry.RegistrateGauge("example_gauge_metric_with_label", "Example gauge metric with label.", new List<string> { "label1", "label2" });


                registry.RegistrateHistogram(Metrics.CreateHistogram("example_histogram_metric", "Example histogram metric."));
                registry.RegistrateHistogram("example_histogram_metric_without_label", "Example histogram metric without label.", new double[] { 1.0, 2.0, 3.0 });
                registry.RegistrateHistogram("example_histogram_metric_with_label", "Example histogram metric with label.", new double[] { 1.0, 2.0, 3.0 }, new List<string> { "label1", "label2" });

                registry.RegistrateSummary(Metrics.CreateSummary("example_summary_metric", "Example summary metric."));
                registry.RegistrateSummary("example_summary_metric_without_label", "Example summary metric without label.");
                registry.RegistrateSummary("example_summary_metric_with_label", "Example summary metric with label.", new List<string> { "label1", "label2" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ICounterManager counterManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseOpenApi(settings =>
            {
                settings.DocumentName = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            });
            app.UseSwaggerUi3(settings =>
            {
                settings.Path = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics();
                endpoints.MapControllers();
            });
        }
    }
}
