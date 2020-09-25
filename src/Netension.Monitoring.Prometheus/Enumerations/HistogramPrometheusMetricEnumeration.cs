using Netension.Core;
using Prometheus;
using System.Collections.Generic;
using System.Linq;

namespace Netension.Monitoring.Prometheus.Enumerations
{
    /// <summary>
    /// <see cref="Enumeration"/> class of a <see cref="Histogram"/> prometheus metric.
    /// </summary>
    public class HistogramPrometheusMetricEnumeration : PrometheusMetricEnumeration
    {
        /// <summary>
        /// Buckets of the metric.
        /// </summary>
        public IEnumerable<double> Buckets { get; }

        /// <summary>
        /// Create an instance from the <see cref="HistogramPrometheusMetricEnumeration"/>.
        /// </summary>
        /// <param name="id">Unique ID of the <see cref="Enumeration"/>.</param>
        /// <param name="name">Name of the <see cref="Histogram"/>.</param>
        /// <param name="description">Description og the <see cref="Histogram"/>.</param>
        public HistogramPrometheusMetricEnumeration(int id, string name, string description)
            : this(id, name, description, Enumerable.Empty<string>(), Enumerable.Empty<double>())
        {

        }

        /// <summary>
        /// Create an instance from the <see cref="HistogramPrometheusMetricEnumeration"/>.
        /// </summary>
        /// <param name="id">Unique ID of the <see cref="Enumeration"/>.</param>
        /// <param name="name">Name of the <see cref="Histogram"/>.</param>
        /// <param name="description">Description of the <see cref="Histogram"/>.</param>
        /// <param name="labels">Labels of the <see cref="Histogram"/>.</param>
        public HistogramPrometheusMetricEnumeration(int id, string name, string description, IEnumerable<string> labels)
            : this(id, name, description, labels, Enumerable.Empty<double>())
        {

        }


        /// <summary>
        /// Create an instance from the <see cref="HistogramPrometheusMetricEnumeration"/>.
        /// </summary>
        /// <param name="id">Unique ID of the <see cref="Enumeration"/>.</param>
        /// <param name="name">Name of the <see cref="Histogram"/>.</param>
        /// <param name="description">Description of the <see cref="Histogram"/>.</param>
        /// <param name="buckets">Buckets of the <see cref="Histogram"/>.</param>
        public HistogramPrometheusMetricEnumeration(int id, string name, string description, IEnumerable<double> buckets)
            : this(id, name, description, Enumerable.Empty<string>(), buckets)
        {

        }


        /// <summary>
        /// Create an instance from the <see cref="HistogramPrometheusMetricEnumeration"/>.
        /// </summary>
        /// <param name="id">Unique ID of the <see cref="Enumeration"/>.</param>
        /// <param name="name">Name of the <see cref="Histogram"/>.</param>
        /// <param name="description">Description of the <see cref="Histogram"/>.</param>
        /// <param name="labels">Labels of the <see cref="Histogram"/>.</param>
        /// <param name="buckets">Buckets of the <see cref="Histogram"/>.</param>
        public HistogramPrometheusMetricEnumeration(int id, string name, string description, IEnumerable<string> labels, IEnumerable<double> buckets)
            : base(id, name, description, labels)
        {
            Buckets = buckets;
        }
    }
}
