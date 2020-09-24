using Netension.Core;
using System.Collections.Generic;
using System.Linq;

namespace Netension.Monitoring.Prometheus.Enumerations
{
    public class PrometheusMetricEnumeration : Enumeration
    {
        public string Description { get; }
        public IEnumerable<string> Labels { get; }

        public PrometheusMetricEnumeration(int id, string name, string description)
            : this(id, name, description, Enumerable.Empty<string>())
        {

        }

        public PrometheusMetricEnumeration(int id, string name, string description, IEnumerable<string> labels) 
            : base(id, name)
        {
            Description = description;
            Labels = labels;
        }
    }
}
