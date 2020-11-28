using Netension.Monitoring.Prometheus.CustomMetrics;
using Prometheus;
using System;

namespace Netension.Monitoring.Prometheus
{
    /// <summary>
    /// Manage <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metrics.
    /// </summary>
    public interface ISummaryManager
    {
        /// <summary>
        /// Get <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> from the collection.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see></param>
        /// <returns>Defintion of the <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric.</returns>
        ///  <exception cref="InvalidOperationException">Throws, if <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> does not exist with given key.</exception>
        Summary this[string name] { get; }

        /// <summary>
        /// Observ a <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric with the given value.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric.</param>
        /// <param name="value">Value of the <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric.</param>
        /// <param name="labels">Labels of the <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric.</param>
        ///  <exception cref="InvalidOperationException">Throws, if <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> does not exist with given key.</exception>
        void Observe(string name, double value, params string[] labels);

        /// <summary>
        /// Start a new duration metric.
        /// </summary>
        /// <param name="name">Name of the metric.</param>
        /// <param name="labels">Labels of the metric.</param>
        /// <returns><see cref="IDurationMetric"/> instance, duration is measured by this instance.</returns>
        /// <example>
        /// <code>
        /// using (summaryCollection.StartDurationMeasurement("Example", "Example")
        /// {
        ///     // SQL Statements
        /// }
        /// </code>
        /// </example>
        IDurationMetric MeasureDuration(string name, params string[] labels);
    }
}
