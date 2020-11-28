using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus.Collections;
using Netension.Monitoring.Prometheus.Managers;
using System;
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
        /// <param name="registrate">Action for registrate metrics.</param>
        public static void AddPrometheusMetrics(this IServiceCollection services, Action<IPrometheusMetricsRegistry, IServiceProvider> registrate)
        {
            services.AddSingleton((provider) =>
            {
                var collection = new PrometheusMetricsCollection();
                var loggerFactory = provider.GetService<ILoggerFactory>();

                registrate(new PrometheusMetricsRegistry(collection, loggerFactory), provider);
                return collection;
            });

            services.AddTransient<ICounterManager, CounterManager>();
            services.AddTransient<IGaugeManager, GaugeManager>();
            services.AddSingleton<ISummaryManager, SummaryManager>();
            services.AddSingleton<IHistogramManager, HistogramManager>();
        }
    }
}
