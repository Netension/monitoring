using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus.Collections;
using Prometheus;

namespace Netension.Monitoring.Prometheus.Managers
{
    internal class GaugeManager : IGaugeManager
    {
        private const string TYPE = "Gauge";
        private readonly PrometheusMetricsCollection _metrics;
        private readonly ILogger<GaugeManager> _logger;

        public GaugeManager(PrometheusMetricsCollection metrics, ILoggerFactory loggerFactory)
        {
            _metrics = metrics;
            _logger = loggerFactory.CreateLogger<GaugeManager>();
        }

        public Gauge this[string name] { get { return _metrics.GetGauge(name)?.Metric; } }

        public void Decrease(string name, params string[] labels)
        {
            Decrease(name, 1.0, labels);
        }

        public void Decrease(string name, double decrement, params string[] labels)
        {
            var metric = this[name];
            if (metric == null)
            {
                _logger.LogWarning("{name} {type} not found.", name, TYPE);
                return;
            }

            _logger.LogDebug("{name} {type} metric decrease with {value}.", name, TYPE, decrement);
            metric.WithLabels(labels).Dec(decrement);
        }

        public void Increase(string name, params string[] labels)
        {
            Increase(name, 1.0, labels);
        }

        public void Increase(string name, double increment, params string[] labels)
        {
            var metric = this[name];
            if (metric == null)
            {
                _logger.LogWarning("{name} {type} not found.", name, TYPE);
                return;
            }

            _logger.LogDebug("{name} {type} metric increase with {value}.", name, TYPE, increment);
            metric.WithLabels(labels).Inc(increment);
        }

        public void Set(string name, double value, params string[] labels)
        {
            var metric = this[name];
            if (metric == null)
            {
                _logger.LogWarning("{name} {type} not found.", name, TYPE);
                return;
            }

            if (metric.Value < value)
            {
                _logger.LogDebug("Increment value of {name} metric to {value}.", name, value);
                metric.WithLabels(labels).IncTo(value);
            }
            else if (metric.Value > value)
            {
                _logger.LogDebug("Decrement value of {name} metric to {value}.", name, value);
                metric.WithLabels(labels).DecTo(value);
            }
        }
    }
}
