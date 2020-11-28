using System;

namespace Netension.Monitoring.Prometheus.CustomMetrics
{
    /// <summary>
    /// Measures the runtime of a snippet of code.
    /// </summary>
    public interface IDurationMetric : IDisposable
    {
        /// <summary>
        /// Name of the metric.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Metric's label values.
        /// </summary>
        string[] Labels { get; }
    }
}
