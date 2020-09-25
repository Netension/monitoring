using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus.CustomMetrics;
using Prometheus;
using System.Collections.Generic;
using System.Linq;

namespace Netension.Monitoring.Prometheus.Containers
{
    /// <inheritdoc/>
    public class PrometheusMetricsCollection : IPrometheusMetricsRegistry, ICounterCollection, IGaugeCollection, IHistogramCollection, ISummaryCollection
    {
        private readonly ILogger<PrometheusMetricsCollection> _logger;

        private readonly ICollection<Counter> _counters = new List<Counter>();
        private readonly ICollection<Gauge> _gauges = new List<Gauge>();
        private readonly ICollection<Summary> _summaries = new List<Summary>();
        private readonly ICollection<Histogram> _histograms = new List<Histogram>();

        /// <inheritdoc/>
        public PrometheusMetricsCollection(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PrometheusMetricsCollection>();
        }

        /// <summary>
        /// Instance of the <see cref="PrometheusMetricsCollection"/>
        /// </summary>
        public static PrometheusMetricsCollection Instance { get; set; }

        #region IPrometheusMetricsRegistry

        /// <inheritdoc/>
        public void RegisterCounter(Counter counter)
        {
            _logger.LogDebug("Register {name} metric as {type}.", counter.Name, "Counter");
            if (_counters.Any(c => c.Name.Equals(counter.Name)))
            {
                _logger.LogWarning("{name} counter metric exists.", counter.Name);
                return;
            }

            _counters.Add(counter);
        }
        /// <inheritdoc/>
        public void RegisterCounter(string name, string description)
        {
            RegisterCounter(name, description, Enumerable.Empty<string>());
        }
        /// <inheritdoc/>
        public void RegisterCounter(string name, string description, IEnumerable<string> labels)
        {
            RegisterCounter(Metrics.CreateCounter(name, description, new CounterConfiguration { LabelNames = labels.ToArray() }));
        }

        /// <inheritdoc/>
        public void RegisterGauge(Gauge gauge)
        {
            _logger.LogDebug("Register {name} metric as {type}.", gauge.Name, "Gauge");
            if (_counters.Any(c => c.Name.Equals(gauge.Name)))
            {
                _logger.LogWarning("{name} gauge metric exists.", gauge.Name);
                return;
            }

            _gauges.Add(gauge);
        }
        /// <inheritdoc/>
        public void RegisterGauge(string name, string description)
        {
            RegisterGauge(name, description, Enumerable.Empty<string>());
        }
        /// <inheritdoc/>
        public void RegisterGauge(string name, string description, IEnumerable<string> labels)
        {
            RegisterGauge(Metrics.CreateGauge(name, description, new GaugeConfiguration { LabelNames = labels.ToArray() }));
        }

        /// <inheritdoc/>
        public void RegisterHistogram(Histogram histogram)
        {
            _logger.LogDebug("Register {name} metric as {type}.", histogram.Name, "Histogram");
            if (_counters.Any(c => c.Name.Equals(histogram.Name)))
            {
                _logger.LogWarning("{name} histogram metric exists.", histogram.Name);
                return;
            }

            _histograms.Add(histogram);
        }
        /// <inheritdoc/>
        public void RegisterHistogram(string name, string description, IEnumerable<double> buckets)
        {
            RegisterHistogram(name, description, buckets, Enumerable.Empty<string>());
        }
        /// <inheritdoc/>
        public void RegisterHistogram(string name, string description, IEnumerable<double> buckets, IEnumerable<string> labels)
        {
            RegisterHistogram(Metrics.CreateHistogram(name, description, new HistogramConfiguration { Buckets = buckets.ToArray(), LabelNames = labels.ToArray() }));
        }

        /// <inheritdoc/>
        public void RegisterSummary(Summary summary)
        {
            _logger.LogDebug("Register {name} metric as {type}.", summary.Name, "Summary");
            if (_counters.Any(c => c.Name.Equals(summary.Name)))
            {
                _logger.LogWarning("{name} summary metric exists.", summary.Name);
                return;
            }

            _summaries.Add(summary);
        }
        /// <inheritdoc/>
        public void RegisterSummary(string name, string description)
        {
            RegisterSummary(name, description, Enumerable.Empty<string>());
        }

        /// <inheritdoc/>
        public void RegisterSummary(string name, string description, IEnumerable<string> labels)
        {
            RegisterSummary(Metrics.CreateSummary(name, description, new SummaryConfiguration { LabelNames = labels.ToArray() }));
        }
        #endregion

        #region ICounterCollection
        Counter ICounterCollection.this[string key]
        {
            get { return _counters.First(c => c.Name.Equals(key)); }
        }

        void ICounterCollection.Increase(string name, params string[] labels)
        {
            ((ICounterCollection)this).Increase(name, 1.0, labels);
        }

        void ICounterCollection.Increase(string name, double increment, params string[] labels)
        {
            _logger.LogDebug("Increase {name} counter metrics with {increment}.", name, increment);
            ((ICounterCollection)this)[name].WithLabels(labels).Inc(increment);
        }

        void ICounterCollection.Set(string name, double value, params string[] labels)
        {
            _logger.LogDebug("Set {name} counter to {value}.", name, value);
            ((ICounterCollection)this)[name].WithLabels(labels).IncTo(value);
        }
        #endregion

        #region IGaugeCollection
        Gauge IGaugeCollection.this[string name]
        {
            get { return _gauges.First(g => g.Name.Equals(name)); }
        }

        void IGaugeCollection.Increase(string name, params string[] labels)
        {
            ((IGaugeCollection)this).Increase(name, 1, labels);
        }

        void IGaugeCollection.Increase(string name, double increment, params string[] labels)
        {
            _logger.LogDebug("Increase {name} gauge metric with {increment}.", name, increment);
            ((IGaugeCollection)this)[name].WithLabels(labels).Inc(increment);
        }

        void IGaugeCollection.Decrease(string name, params string[] labels)
        {
            ((IGaugeCollection)this).Decrease(name, 1, labels);
        }

        void IGaugeCollection.Decrease(string name, double decrement, params string[] labels)
        {
            _logger.LogDebug("Decrease {name} gauge metric with {decrement}.", name, decrement);
            ((IGaugeCollection)this)[name].WithLabels(labels).Dec(decrement);
        }

        void IGaugeCollection.Set(string name, double value, params string[] labels)
        {
            var gauge = ((IGaugeCollection)this)[name];
            if (gauge.Value < value)
            {
                _logger.LogDebug("Increase {name} gauge metric to {value}.", name, value);
                gauge.WithLabels(labels).IncTo(value);

            }
            else
            {
                _logger.LogDebug("Decrease {name} gauge metric to {value}.", name, value);
                gauge.WithLabels(labels).DecTo(value);
            }
        }
        #endregion

        #region IHistogramCollection
        Histogram IHistogramCollection.this[string name]
        {
            get { return _histograms.First(h => h.Name.Equals(name)); }
        }

        void IHistogramCollection.Observe(string name, double value, params string[] labels)
        {
            _logger.LogDebug("Observe {name} histogram metric with {value}.", name, value);
            ((IHistogramCollection)this)[name].WithLabels(labels).Observe(value);
        }
        #endregion

        #region ISummaryCollection
        Summary ISummaryCollection.this[string name]
        {
            get { return _summaries.First(s => s.Name.Equals(name)); }
        }

        void ISummaryCollection.Observe(string name, double value, params string[] labels)
        {
            _logger.LogDebug("Observe {name} summary metric with {value}.", name, value);
            ((ISummaryCollection)this)[name].WithLabels(labels).Observe(value);
        }

        IDurationMetric ISummaryCollection.StartDurationMeasurement(string name, params string[] labels)
        {
            return new DurationMetric(this, name, labels);
        }
        #endregion
    }
}
