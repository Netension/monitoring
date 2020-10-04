using Netension.Monitoring.Core.Models;
using Prometheus;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Netension.Monitoring.Prometheus.Collections
{
    [ExcludeFromCodeCoverage]
    public class PrometheusMetricsCollection : ICollection<MetricDefinition<Counter>>, ICollection<MetricDefinition<Gauge>>, ICollection<MetricDefinition<Histogram>>, ICollection<MetricDefinition<Summary>>
    {
        private readonly ICollection<MetricDefinition<Counter>> _counters = new List<MetricDefinition<Counter>>();

        private readonly ICollection<MetricDefinition<Gauge>> _gauges = new List<MetricDefinition<Gauge>>();
        private readonly ICollection<MetricDefinition<Histogram>> _histograms = new List<MetricDefinition<Histogram>>();
        private readonly ICollection<MetricDefinition<Summary>> _summaries = new List<MetricDefinition<Summary>>();

        public int Count { get { return _counters.Count + _gauges.Count + _histograms.Count + _summaries.Count; } }
        public bool IsReadOnly => false;

        public MetricDefinition<Counter> GetCounter(string name)
        {
            return _counters.FirstOrDefault(c => c.Key.Equals(name));
        }
        public MetricDefinition<Gauge> GetGauge(string name)
        {
            return _gauges.FirstOrDefault(c => c.Key.Equals(name));
        }

        public bool Contains(string key)
        {
            return _counters.Any(c => c.Key.Equals(key)) ||
                _gauges.Any(g => g.Key.Equals(key)) ||
                _histograms.Any(h => h.Key.Equals(key)) ||
                _summaries.Any(s => s.Key.Equals(key));
        }

        public void Add(MetricDefinition<Counter> item)
        {
            _counters.Add(item);
        }

        public void Add(MetricDefinition<Gauge> item)
        {
            _gauges.Add(item);
        }

        public void Clear()
        {
            _counters.Clear();
        }

        public bool Contains(MetricDefinition<Counter> item)
        {
            return _counters.Contains(item);
        }

        public bool Contains(MetricDefinition<Gauge> item)
        {
            return _gauges.Contains(item);
        }

        public void CopyTo(MetricDefinition<Counter>[] array, int arrayIndex)
        {
            _counters.CopyTo(array, arrayIndex);
        }

        public void CopyTo(MetricDefinition<Gauge>[] array, int arrayIndex)
        {
            _gauges.CopyTo(array, arrayIndex);
        }

        IEnumerator<MetricDefinition<Counter>> IEnumerable<MetricDefinition<Counter>>.GetEnumerator()
        {
            return _counters.GetEnumerator();
        }

        public bool Remove(MetricDefinition<Counter> item)
        {
            return _counters.Remove(item);
        }

        public bool Remove(MetricDefinition<Gauge> item)
        {
            return _gauges.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _counters.GetEnumerator();
        }

        IEnumerator<MetricDefinition<Gauge>> IEnumerable<MetricDefinition<Gauge>>.GetEnumerator()
        {
            return _gauges.GetEnumerator();
        }

        public void Add(MetricDefinition<Histogram> item)
        {
            _histograms.Add(item);
        }

        public bool Contains(MetricDefinition<Histogram> item)
        {
            return _histograms.Contains(item);
        }

        public void CopyTo(MetricDefinition<Histogram>[] array, int arrayIndex)
        {
            _histograms.CopyTo(array, arrayIndex);
        }

        public bool Remove(MetricDefinition<Histogram> item)
        {
            return _histograms.Remove(item);
        }

        public IEnumerator<MetricDefinition<Histogram>> GetEnumerator()
        {
            return _histograms.GetEnumerator();
        }

        public void Add(MetricDefinition<Summary> item)
        {
            _summaries.Add(item);
        }

        public bool Contains(MetricDefinition<Summary> item)
        {
            return _summaries.Contains(item);
        }

        public void CopyTo(MetricDefinition<Summary>[] array, int arrayIndex)
        {
            _summaries.CopyTo(array, arrayIndex);
        }

        public bool Remove(MetricDefinition<Summary> item)
        {
            return _summaries.Remove(item);
        }

        IEnumerator<MetricDefinition<Summary>> IEnumerable<MetricDefinition<Summary>>.GetEnumerator()
        {
            return _summaries.GetEnumerator();
        }
    }
}
