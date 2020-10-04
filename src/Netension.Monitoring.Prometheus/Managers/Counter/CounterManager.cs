using Netension.Monitoring.Prometheus.Collections;
using Prometheus;

namespace Netension.Monitoring.Prometheus.Managers
{
    /// <inheritdoc/>
    internal class CounterManager : ICounterManager
    {
        private readonly PrometheusMetricsCollection _metrics;

        /// <inheritdoc/>
        public CounterManager(PrometheusMetricsCollection metrics)
        {
            _metrics = metrics;
        }

        /// <inheritdoc/>
        public Counter this[string name] { get { return _metrics.GetCounter(name).Metric; } }

        /// <inheritdoc/>
        public void Increase(string name, params string[] labels)
        {
            this[name].WithLabels(labels).Inc(1.0);
        }

        /// <inheritdoc/>
        public void Increase(string name, double increment, params string[] labels)
        {
            this[name].WithLabels(labels).Inc(increment);
        }

        /// <inheritdoc/>
        public void Set(string name, double value, params string[] labels)
        {
            this[name].WithLabels(labels).IncTo(value);
        }
    }
}
