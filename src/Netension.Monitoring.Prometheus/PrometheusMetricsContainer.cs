//using Microsoft.Extensions.Logging;
//using Netension.Monitoring.Core.Models;
//using Netension.Monitoring.Prometheus.Collections;
//using Netension.Monitoring.Prometheus.CustomMetrics;
//using Prometheus;
//using System.Collections.Generic;
//using System.Linq;

//namespace Netension.Monitoring.Prometheus.Containers
//{
//    /// <inheritdoc/>
//    internal class PrometheusMetricsContainer
//    {
//        private readonly ILogger<PrometheusMetricsContainer> _logger;
//        private readonly PrometheusMetricsCollection _collection;

//        /// <inheritdoc/>
//        public PrometheusMetricsContainer(PrometheusMetricsCollection collection, ILoggerFactory loggerFactory)
//        {
//            _logger = loggerFactory.CreateLogger<PrometheusMetricsContainer>();
//            _collection = collection;
//        }

//        #region IPrometheusMetricsRegistry

//        /// <inheritdoc/>
//        public void RegistrateCounter(Counter counter)
//        {
//            _logger.LogDebug("Register {name} metric as {type}.", counter.Name, "Counter");
//            if (_collection.Contains(counter.Name))
//            {
//                _logger.LogWarning("{name} counter metric exists.", counter.Name);
//                return;
//            }

//            _collection.Add(new MetricDefinition<Counter>(counter.Name, counter));
//        }
//        /// <inheritdoc/>
//        public void RegistrateCounter(string name, string description)
//        {
//            RegistrateCounter(name, description, Enumerable.Empty<string>());
//        }
//        /// <inheritdoc/>
//        public void RegistrateCounter(string name, string description, IEnumerable<string> labels)
//        {
//            RegistrateCounter(Metrics.CreateCounter(name, description, new CounterConfiguration { LabelNames = labels.ToArray() }));
//        }

//        /// <inheritdoc/>
//        public void RegistrateGauge(Gauge gauge)
//        {
//            _logger.LogDebug("Register {name} metric as {type}.", gauge.Name, "Gauge");
//            if (_collection.Contains(gauge.Name))
//            {
//                _logger.LogWarning("{name} gauge metric exists.", gauge.Name);
//                return;
//            }

//            _collection.Add(new MetricDefinition<Gauge>(gauge.Name, gauge));
//        }
//        /// <inheritdoc/>
//        public void RegistrateGauge(string name, string description)
//        {
//            RegistrateGauge(name, description, Enumerable.Empty<string>());
//        }
//        /// <inheritdoc/>
//        public void RegistrateGauge(string name, string description, IEnumerable<string> labels)
//        {
//            RegistrateGauge(Metrics.CreateGauge(name, description, new GaugeConfiguration { LabelNames = labels.ToArray() }));
//        }

//        /// <inheritdoc/>
//        public void RegistrateHistogram(Histogram histogram)
//        {
//            _logger.LogDebug("Register {name} metric as {type}.", histogram.Name, "Histogram");
//            if (_histograms.Any(c => c.Name.Equals(histogram.Name)))
//            {
//                _logger.LogWarning("{name} histogram metric exists.", histogram.Name);
//                return;
//            }

//            _histograms.Add(histogram);
//        }
//        /// <inheritdoc/>
//        public void RegistrateHistogram(string name, string description, IEnumerable<double> buckets)
//        {
//            RegistrateHistogram(name, description, buckets, Enumerable.Empty<string>());
//        }
//        /// <inheritdoc/>
//        public void RegistrateHistogram(string name, string description, IEnumerable<double> buckets, IEnumerable<string> labels)
//        {
//            RegistrateHistogram(Metrics.CreateHistogram(name, description, new HistogramConfiguration { Buckets = buckets.ToArray(), LabelNames = labels.ToArray() }));
//        }

//        /// <inheritdoc/>
//        public void RegistrateSummary(Summary summary)
//        {
//            _logger.LogDebug("Register {name} metric as {type}.", summary.Name, "Summary");
//            if (_summaries.Any(c => c.Name.Equals(summary.Name)))
//            {
//                _logger.LogWarning("{name} summary metric exists.", summary.Name);
//                return;
//            }

//            _summaries.Add(summary);
//        }
//        /// <inheritdoc/>
//        public void RegistrateSummary(string name, string description)
//        {
//            RegistrateSummary(name, description, Enumerable.Empty<string>());
//        }

//        /// <inheritdoc/>
//        public void RegistrateSummary(string name, string description, IEnumerable<string> labels)
//        {
//            RegistrateSummary(Metrics.CreateSummary(name, description, new SummaryConfiguration { LabelNames = labels.ToArray() }));
//        }
//        #endregion

//        #region ICounterCollection
//        Counter ICounterManager.this[string key]
//        {
//            get { return _counters.First(c => c.Name.Equals(key)); }
//        }

//        void ICounterManager.Increase(string name, params string[] labels)
//        {
//            ((ICounterManager)this).Increase(name, 1.0, labels);
//        }

//        void ICounterManager.Increase(string name, double increment, params string[] labels)
//        {
//            _logger.LogDebug("Increase {name} counter metrics with {increment}.", name, increment);
//            ((ICounterManager)this)[name].WithLabels(labels).Inc(increment);
//        }

//        void ICounterManager.Set(string name, double value, params string[] labels)
//        {
//            _logger.LogDebug("Set {name} counter to {value}.", name, value);
//            ((ICounterManager)this)[name].WithLabels(labels).IncTo(value);
//        }
//        #endregion

//        #region IGaugeCollection
//        Gauge IGaugeManager.this[string name]
//        {
//            get { return _gauges.First(g => g.Name.Equals(name)); }
//        }

//        void IGaugeManager.Increase(string name, params string[] labels)
//        {
//            ((IGaugeManager)this).Increase(name, 1, labels);
//        }

//        void IGaugeManager.Increase(string name, double increment, params string[] labels)
//        {
//            _logger.LogDebug("Increase {name} gauge metric with {increment}.", name, increment);
//            ((IGaugeManager)this)[name].WithLabels(labels).Inc(increment);
//        }

//        void IGaugeManager.Decrease(string name, params string[] labels)
//        {
//            ((IGaugeManager)this).Decrease(name, 1, labels);
//        }

//        void IGaugeManager.Decrease(string name, double decrement, params string[] labels)
//        {
//            _logger.LogDebug("Decrease {name} gauge metric with {decrement}.", name, decrement);
//            ((IGaugeManager)this)[name].WithLabels(labels).Dec(decrement);
//        }

//        void IGaugeManager.Set(string name, double value, params string[] labels)
//        {
//            var gauge = ((IGaugeManager)this)[name];
//            if (gauge.Value < value)
//            {
//                _logger.LogDebug("Increase {name} gauge metric to {value}.", name, value);
//                gauge.WithLabels(labels).IncTo(value);

//            }
//            else
//            {
//                _logger.LogDebug("Decrease {name} gauge metric to {value}.", name, value);
//                gauge.WithLabels(labels).DecTo(value);
//            }
//        }
//        #endregion

//        #region IHistogramCollection
//        Histogram IHistogramCollection.this[string name]
//        {
//            get { return _histograms.First(h => h.Name.Equals(name)); }
//        }

//        void IHistogramCollection.Observe(string name, double value, params string[] labels)
//        {
//            _logger.LogDebug("Observe {name} histogram metric with {value}.", name, value);
//            ((IHistogramCollection)this)[name].WithLabels(labels).Observe(value);
//        }
//        #endregion

//        #region ISummaryCollection
//        Summary ISummaryCollection.this[string name]
//        {
//            get { return _summaries.First(s => s.Name.Equals(name)); }
//        }

//        void ISummaryCollection.Observe(string name, double value, params string[] labels)
//        {
//            _logger.LogDebug("Observe {name} summary metric with {value}.", name, value);
//            ((ISummaryCollection)this)[name].WithLabels(labels).Observe(value);
//        }

//        IDurationMetric ISummaryCollection.StartDurationMeasurement(string name, params string[] labels)
//        {
//            return new DurationMetric(((ISummaryCollection)this)[name], name, labels);
//        }
//        #endregion
//    }
//}
