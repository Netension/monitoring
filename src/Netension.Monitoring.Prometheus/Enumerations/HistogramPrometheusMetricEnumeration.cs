using System.Collections.Generic;
using System.Linq;

namespace Netension.Monitoring.Prometheus.Enumerations
{
    public class HistogramPrometheusMetricEnumeration : PrometheusMetricEnumeration
    {
        public IEnumerable<double> Buckets { get; }

        public HistogramPrometheusMetricEnumeration(int id, string name, string description)
            : this(id, name, description, Enumerable.Empty<string>(), Enumerable.Empty<double>())
        {

        }

        public HistogramPrometheusMetricEnumeration(int id, string name, string description, IEnumerable<string> labels)
            : this(id, name, description, labels, Enumerable.Empty<double>())
        {

        }

        public HistogramPrometheusMetricEnumeration(int id, string name, string description, IEnumerable<double> buckets)
            : this(id, name, description, Enumerable.Empty<string>(), buckets)
        {

        }

        public HistogramPrometheusMetricEnumeration(int id, string name, string description, IEnumerable<string> labels, IEnumerable<double> buckets) 
            : base(id, name, description, labels)
        {
            Buckets = buckets;
        }
    }
}
