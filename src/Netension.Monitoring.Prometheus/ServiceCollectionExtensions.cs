using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus.Containers;

namespace Netension.Monitoring.Prometheus
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPrometheusMetricsCollection(this IServiceCollection services, ILoggerFactory loggerFactory)
        {
            var collection = new PrometheusMetricsCollection(loggerFactory);
            PrometheusMetricsCollection.Instance = collection;

            services.AddSingleton<IPrometheusMetricsRegistry>(collection);
            services.AddSingleton<ICounterCollection>(collection);
            services.AddSingleton<IGaugeCollection>(collection);
            services.AddSingleton<ISummaryCollection>(collection);
            services.AddSingleton<IHistogramCollection>(collection);

            return services;
        }
    }
}
