using Microsoft.Extensions.Logging;
using Netension.Monitoring.Core.Models;
using Netension.Monitoring.Prometheus.Collections;
using Prometheus;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Netension.Monitoring.Prometheus
{
    public class PrometheusMetricsRegistry : IPrometheusMetricsRegistry
    {
        private readonly ILogger<PrometheusMetricsRegistry> _logger;
        private readonly PrometheusMetricsCollection _metrics;

        [ExcludeFromCodeCoverage]
        public ICounterManager CounterManager { get; }
        [ExcludeFromCodeCoverage]
        public IGaugeManager GaugeManager { get; }

        public PrometheusMetricsRegistry(PrometheusMetricsCollection metrics, ICounterManager counterManager, IGaugeManager gaugeManager, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PrometheusMetricsRegistry>();
            _metrics = metrics;
            CounterManager = counterManager;
            GaugeManager = gaugeManager;
        }

        /// <inheritdoc/>
        public IPrometheusMetricsRegistry RegistrateCounter(Counter counter)
        {
            _logger.LogDebug("Registrate {name} {type} metric.", counter.Name, "Counter");
            _metrics.Add(new MetricDefinition<Counter>(counter.Name, counter));
            return this;
        }

        /// <inheritdoc/>
        public IPrometheusMetricsRegistry RegistrateCounter(string name, string description)
        {
            return RegistrateCounter(name, description, Enumerable.Empty<string>());
        }

        /// <inheritdoc/>
        public IPrometheusMetricsRegistry RegistrateCounter(string name, string description, IEnumerable<string> labels)
        {
            if (_metrics.Contains(name))
            {
                _logger.LogWarning("{name} metric has already registrated.", name);
                return this;
            }

            return RegistrateCounter(Metrics.CreateCounter(name, description, labels.ToArray()));
        }

        /// <inheritdoc/>
        public IPrometheusMetricsRegistry RegistrateGauge(Gauge gauge)
        {
            _logger.LogDebug("Registrate {name} {type} metric.", gauge.Name, "Gauge");
            _metrics.Add(new MetricDefinition<Gauge>(gauge.Name, gauge));
            return this;
        }

        /// <inheritdoc/>
        public IPrometheusMetricsRegistry RegistrateGauge(string name, string description)
        {
            return RegistrateGauge(name, description, Enumerable.Empty<string>());
        }

        /// <inheritdoc/>
        public IPrometheusMetricsRegistry RegistrateGauge(string name, string description, IEnumerable<string> labels)
        {
            if (_metrics.Contains(name))
            {
                _logger.LogWarning("{name} metric has already registrated.", name);
                return this;
            }

            return RegistrateGauge(Metrics.CreateGauge(name, description, labels.ToArray()));
        }

        /// <inheritdoc/>
        public IPrometheusMetricsRegistry RegistrateHistogram(Histogram histogram)
        {
            _logger.LogDebug("Registrate {name} {type} metric.", histogram.Name, "Histogram");
            _metrics.Add(new MetricDefinition<Histogram>(histogram.Name, histogram));
            return this;
        }

        /// <inheritdoc/>
        public IPrometheusMetricsRegistry RegistrateHistogram(string name, string description, IEnumerable<double> buckets)
        {
            return RegistrateHistogram(name, description, buckets, Enumerable.Empty<string>());
        }

        /// <inheritdoc/>
        public IPrometheusMetricsRegistry RegistrateHistogram(string name, string description, IEnumerable<double> buckets, IEnumerable<string> labels)
        {
            if (_metrics.Contains(name))
            {
                _logger.LogWarning("{name} metric has already registrated.", name);
                return this;
            }
            return RegistrateHistogram(Metrics.CreateHistogram(name, description, new HistogramConfiguration { Buckets = buckets.ToArray(), LabelNames = labels.ToArray() }));
        }

        /// <inheritdoc/>
        public IPrometheusMetricsRegistry RegistrateSummary(Summary summary)
        {
            _logger.LogDebug("Registrate {name} {type} metric.", summary.Name, "Summary");
            _metrics.Add(new MetricDefinition<Summary>(summary.Name, summary));
            return this;
        }

        /// <inheritdoc/>
        public IPrometheusMetricsRegistry RegistrateSummary(string name, string description)
        {
            return RegistrateSummary(name, description, Enumerable.Empty<string>());
        }

        /// <inheritdoc/>
        public IPrometheusMetricsRegistry RegistrateSummary(string name, string description, IEnumerable<string> labels)
        {
            if (_metrics.Contains(name))
            {
                _logger.LogWarning("{name} metric has already registrated.", name);
                return this;
            }
            return RegistrateSummary(Metrics.CreateSummary(name, description, labels.ToArray()));
        }
    }
}
