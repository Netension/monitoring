using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus.Containers;

namespace Netension.Monitoring.Prometheus
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ServiceCollectionExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Registers Prometheus metrics's objects.
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> instance.</param>
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
        public static IServiceCollection AddPrometheusMetricsCollection(this IServiceCollection services, ILoggerFactory loggerFactory)
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
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
