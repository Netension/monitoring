using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus.Containers;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Netension.Monitoring.Prometheus
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Registers Prometheus metrics's objects.
        /// </summary>
        /// <param name="registrate">Registrate metrics to be used.</param>
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
        public static IServiceCollection AddPrometheusMetrics(this IServiceCollection services, Action<IPrometheusMetricsRegistry, IServiceProvider> registrate)
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
        {

            services.AddSingleton((context) =>
            {
                PrometheusMetricsCollection.Instance = new PrometheusMetricsCollection(context.GetService<ILoggerFactory>());

                registrate(PrometheusMetricsCollection.Instance, context);

                return PrometheusMetricsCollection.Instance;
            });

            services.AddSingleton<ICounterCollection>(PrometheusMetricsCollection.Instance);
            services.AddSingleton<IGaugeCollection>(PrometheusMetricsCollection.Instance);
            services.AddSingleton<ISummaryCollection>(PrometheusMetricsCollection.Instance);
            services.AddSingleton<IHistogramCollection>(PrometheusMetricsCollection.Instance);

            return services;
        }
    }
}
