using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus.Collections;
using Netension.Monitoring.Prometheus.Managers;
using System.Diagnostics.CodeAnalysis;

namespace Netension.Monitoring.Prometheus
{
    [ExcludeFromCodeCoverage]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ServiceCollectionExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Registrate an instance of <see cref="ICounterManager"/>, <see cref="IGaugeManager"/>, <see cref="ISummaryManager"/> and <see cref="IHistogramManager"/>.
        /// </summary>
        /// <returns>Returns with <see cref="IPrometheusMetricsRegistry"/>. It makes to possible to registrate the neccessary metrics.</returns>
        public static IPrometheusMetricsRegistry AddPrometheusMetrics(this IServiceCollection services, ILoggerFactory loggerFactory)
        {
            var collection = new PrometheusMetricsCollection();

            services.AddTransient<ICounterManager>((context) => new CounterManager(collection, loggerFactory));
            services.AddTransient<IGaugeManager>((context) => new GaugeManager(collection, loggerFactory));
            services.AddSingleton<ISummaryManager>((context) => new SummaryManager(collection, loggerFactory));
            services.AddSingleton<IHistogramManager>((context) => new HistogramManager(collection, loggerFactory));

            return new PrometheusMetricsRegistry(collection, loggerFactory);
        }
    }
}
