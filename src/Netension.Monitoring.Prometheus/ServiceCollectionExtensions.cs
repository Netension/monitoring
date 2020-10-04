using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus.Collections;
using Netension.Monitoring.Prometheus.Managers;

namespace Netension.Monitoring.Prometheus
{
    public static class ServiceCollectionExtensions
    {
        public static IPrometheusMetricsRegistry AddPrometheusMetrics(this IServiceCollection services, ILoggerFactory loggerFactory)
        {
            var collection = new PrometheusMetricsCollection();

            services.AddTransient<ICounterManager>((context) => new CounterManager(collection, loggerFactory));
            services.AddTransient<IGaugeManager>((context) => new GaugeManager(collection, loggerFactory));
            //services.AddSingleton<ISummaryCollection>((context) => context.GetRequiredService<PrometheusMetricsContainer>());
            //services.AddSingleton<IHistogramCollection>((context) => context.GetRequiredService<PrometheusMetricsContainer>());

            return new PrometheusMetricsRegistry(collection, loggerFactory);
        }
    }
}
