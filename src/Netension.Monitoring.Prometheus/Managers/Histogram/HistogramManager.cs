using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus.Collections;
using Prometheus;

namespace Netension.Monitoring.Prometheus.Managers
{
    internal class HistogramManager : IHistogramManager
    {
        private const string TYPE = "Histogram";
        private readonly PrometheusMetricsCollection _metrics;
        private readonly ILogger<HistogramManager> _logger;

        public HistogramManager(PrometheusMetricsCollection metrics, ILoggerFactory loggerFactory)
        {
            _metrics = metrics;
            _logger = loggerFactory.CreateLogger<HistogramManager>();
        }

        public Histogram this[string name] { get { return _metrics.GetHistogram(name)?.Metric; } }

        public void Observe(string name, double value, params string[] labels)
        {
            var metric = this[name];
            if (metric == null)
            {
                _logger.LogWarning("{name} {type} metric not found.", name, TYPE);
                return;
            }

            _logger.LogDebug("{name} {type} metric observe with {value}.", name, TYPE, value);
            metric.WithLabels(labels).Observe(value);
        }
    }
}
