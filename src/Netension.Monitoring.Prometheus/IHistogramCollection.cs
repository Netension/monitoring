﻿using Prometheus;

namespace Netension.Monitoring.Prometheus
{
    /// <summary>
    /// Manage <see href="https://prometheus.io/docs/concepts/metric_types/#histogram">Histogram</see> metrics.
    /// </summary>
    public interface IHistogramCollection
    {
        /// <summary>
        /// Get <see href="https://prometheus.io/docs/concepts/metric_types/#histogram">Histogram</see> from the collection.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#histogram">Histogram</see> metric.</param>
        /// <returns>Defintion of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Histogram</see> metric.</returns>
        Histogram this[string name] { get; }

        /// <summary>
        /// Observs a <see href="https://prometheus.io/docs/concepts/metric_types/#histogram">Histogram</see> metric with the given value.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#histogram">Histogram</see> metric.</param>
        /// <param name="value">Value of the <see href="https://prometheus.io/docs/concepts/metric_types/#histogram">Histogram</see> metric.</param>
        /// <param name="labels">Labels of the <see href="https://prometheus.io/docs/concepts/metric_types/#histogram">Histogram</see> metric.</param>
        void Observe(string name, double value, params string[] labels);
    }
}
