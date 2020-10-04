namespace Netension.Monitoring.Core.Models
{
    public class MetricDefinition<TMetric>
    {
        public string Key { get; }
        public TMetric Metric { get; }

        public MetricDefinition(string key, TMetric metric)
        {
            Key = key;
            Metric = metric;
        }
    }
}
