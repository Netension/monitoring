namespace Netension.Monitoring.Core.Models
{
    /// <summary>
    /// Define a metric.
    /// </summary>
    /// <typeparam name="TMetric">Type of the metric object.</typeparam>
    public class MetricDefinition<TMetric>
    {
        /// <summary>
        /// Key of the metric.
        /// </summary>
        public string Key { get; }
        /// <summary>
        /// Metric instance.
        /// </summary>
        public TMetric Metric { get; }

        /// <summary>
        /// Create a new <see cref="MetricDefinition{TMetric}"/> instance.
        /// </summary>
        /// <param name="key">Key of the metric.</param>
        /// <param name="metric">Metric instance.</param>
        public MetricDefinition(string key, TMetric metric)
        {
            Key = key;
            Metric = metric;
        }
    }
}
