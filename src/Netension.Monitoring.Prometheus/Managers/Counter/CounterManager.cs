using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus.Collections;
using Prometheus;

namespace Netension.Monitoring.Prometheus.Managers
{
    /// <inheritdoc/>
    internal class CounterManager : ICounterManager
    {
        private const string TYPE = "Counter";
        private readonly PrometheusMetricsCollection _metrics;
        private readonly ILogger<CounterManager> _logger;

        /// <inheritdoc/>
        public CounterManager(PrometheusMetricsCollection metrics, ILoggerFactory loggerFactory)
        {
            _metrics = metrics;
            _logger = loggerFactory.CreateLogger<CounterManager>();
        }

        /// <inheritdoc/>
        public Counter this[string name] { get { return _metrics.GetCounter(name)?.Metric; } }

        /// <inheritdoc/>
        public void Increase(string name, params string[] labels)
        {
            Increase(name, 1.0, labels);
        }

        /// <inheritdoc/>
        public void Increase(string name, double increment, params string[] labels)
        {
            var metric = this[name];
            if (metric == null)
            {
                _logger.LogWarning("{name} {type} metric not found.", name, TYPE);
                return;
            }

            _logger.LogDebug("{name} {type} metric increase with {value}.", name, TYPE, increment);
            metric.WithLabels(labels).Inc(increment);
        }

        /// <inheritdoc/>
        public void Set(string name, double value, params string[] labels)
        {
            var metric = this[name];
            if (metric == null)
            {
                _logger.LogWarning("{name} {type} metric not found.", name, TYPE);
                return;
            }

            metric.WithLabels(labels).IncTo(value);
        }
    }
}
