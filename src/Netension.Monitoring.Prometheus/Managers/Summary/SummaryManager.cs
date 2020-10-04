using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus.Collections;
using Netension.Monitoring.Prometheus.CustomMetrics;
using Prometheus;

namespace Netension.Monitoring.Prometheus.Managers
{
    internal class SummaryManager : ISummaryManager
    {
        private const string TYPE = "Summary";
        private readonly PrometheusMetricsCollection _metrics;
        private readonly ILogger<SummaryManager> _logger;

        public SummaryManager(PrometheusMetricsCollection metrics, ILoggerFactory loggerFactory)
        {
            _metrics = metrics;
            _logger = loggerFactory.CreateLogger<SummaryManager>();
        }

        public Summary this[string name] { get { return _metrics.GetSummary(name)?.Metric; } }

        public void Observe(string name, double value, params string[] labels)
        {
            var metric = this[name];
            if (metric == null)
            {
                _logger.LogWarning("{name} {type} metric not found.", name, TYPE);
                return;
            }

            _logger.LogDebug("{name} {type} metric observe with {value}.", name, TYPE, value);
            metric.Observe(value);
        }

        public IDurationMetric MeasureDuration(string name, params string[] labels)
        {
            var metric = this[name];
            if (metric == null)
            {
                _logger.LogWarning("{name} {type} metric not found.", name, TYPE);
                return null;
            }

            _logger.LogDebug("Start duration measurement for {name} metric.", name);
            return new DurationMetric(metric, name, labels);
        }
    }
}
