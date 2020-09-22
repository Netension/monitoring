using Microsoft.Extensions.Logging;
using Prometheus;
using System.Collections.Generic;
using System.Linq;

namespace Netension.Monitoring.Prometheus.Containers
{
    internal class PrometheusMetricsContainer : IPrometheusMetricsRegistry, ICounter
    {
        private readonly ILogger _logger;

        private readonly ICollection<Counter> _counters = new List<Counter>();
        private readonly ICollection<Gauge> _gauges = new List<Gauge>();
        private readonly ICollection<Summary> _summaries = new List<Summary>();
        private readonly ICollection<Histogram> _histograms = new List<Histogram>();

        public PrometheusMetricsContainer(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PrometheusMetricsContainer>();
        }

        public void Increase(string key)
        {
            _counters.First(c => c.Name.Equals(key)).Inc();
        }

        public void Increase(string key, double value)
        {
            _counters.First(c => c.Name.Equals(key)).Inc(value);
        }

        public void RegisterCounter(string name, string description)
        {
            RegisterCounter(name, description, Enumerable.Empty<string>());
        }

        public void RegisterCounter(string name, string description, IEnumerable<string> labels)
        {
            _logger.LogDebug("Register {name} metric as {type}.", name, "Counter");
            _counters.Add(Metrics.CreateCounter(name, description, new CounterConfiguration { LabelNames = labels.ToArray() }));
        }

        public void RegisterGauge(string name, string description)
        {
            RegisterGauge(name, description, Enumerable.Empty<string>());
        }

        public void RegisterGauge(string name, string description, IEnumerable<string> labels)
        {
            _logger.LogDebug("Register {name} metric as {type}.", name, "Gauge");
            _gauges.Add(Metrics.CreateGauge(name, description, new GaugeConfiguration { LabelNames = labels.ToArray() }));
        }

        public void RegisterHistogram(string name, string description, IEnumerable<double> buckets)
        {
            RegisterHistogram(name, description, buckets, Enumerable.Empty<string>());
        }

        public void RegisterHistogram(string name, string description, IEnumerable<double> buckets, IEnumerable<string> labels)
        {
            _logger.LogDebug("Register {name} metric as {type}.", name, "Histogram");
            _histograms.Add(Metrics.CreateHistogram(name, description, new HistogramConfiguration { Buckets = buckets.ToArray(), LabelNames = labels.ToArray() }));
        }

        public void RegisterSummary(string name, string description)
        {
            RegisterSummary(name, description, Enumerable.Empty<string>());
        }

        public void RegisterSummary(string name, string description, IEnumerable<string> labels)
        {
            _logger.LogDebug("Register {name} metric as {type}.", name, "Summary");
            _summaries.Add(Metrics.CreateSummary(name, description, new SummaryConfiguration { LabelNames = labels.ToArray() }));
        }
    }
}
