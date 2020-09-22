using System.Collections.Generic;

namespace Netension.Monitoring.Prometheus
{
    public interface IPrometheusMetricsRegistry
    {
        void RegisterCounter(string name, string description);
        void RegisterCounter(string name, string description, IEnumerable<string> labels);

        void RegisterGauge(string name, string description);
        void RegisterGauge(string name, string description, IEnumerable<string> labels);

        void RegisterHistogram(string name, string description, IEnumerable<double> buckets);
        void RegisterHistogram(string name, string description, IEnumerable<double> buckets, IEnumerable<string> labels);

        void RegisterSummary(string name, string description);
        void RegisterSummary(string name, string description, IEnumerable<string> labels);
    }
}
