using Netension.Core;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Netension.Monitoring.Prometheus.Enumerations
{

    /// <summary>
    /// <see cref="Enumeration"/> class of prometheus metric.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PrometheusMetricEnumeration : Enumeration
    {
        /// <summary>
        /// Description of the metric.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Label values of the metric.
        /// </summary>
        public IEnumerable<string> Labels { get; }

        /// <summary>
        /// Create an instance from the <see cref="HistogramPrometheusMetricEnumeration"/>.
        /// </summary>
        /// <param name="id">Unique ID of the <see cref="Enumeration"/>.</param>
        /// <param name="name">Name of the metric.</param>
        /// <param name="description">Description of the metric.</param>
        public PrometheusMetricEnumeration(int id, string name, string description)
            : this(id, name, description, Enumerable.Empty<string>())
        {

        }
        /// <summary>
        /// Create an instance from the <see cref="HistogramPrometheusMetricEnumeration"/>.
        /// </summary>
        /// <param name="id">Unique ID of the <see cref="Enumeration"/>.</param>
        /// <param name="name">Name of the metric.</param>
        /// <param name="description">Description of the metric.</param>
        /// <param name="labels">Label values of the metric.</param>
        public PrometheusMetricEnumeration(int id, string name, string description, IEnumerable<string> labels)
            : base(id, name)
        {
            Description = description;
            Labels = labels;
        }
    }
}
