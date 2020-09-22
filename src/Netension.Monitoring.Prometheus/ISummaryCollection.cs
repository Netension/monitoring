using Prometheus;

namespace Netension.Monitoring.Prometheus
{
    /// <summary>
    /// Manage <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metrics.
    /// </summary>
    public interface ISummaryCollection
    {
        /// <summary>
        /// Get <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> from the collection.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see></param>
        /// <returns>Defintion of the <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric.</returns>
        Summary this[string name] { get; }

        /// <summary>
        /// Observ a <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric with the given value.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric.</param>
        /// <param name="value">Value of the <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric.</param>
        /// <param name="labels">Labels of the <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric.</param>
        void Observe(string name, double value, params string[] labels);
    }
}
