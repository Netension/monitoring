using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Netension.Monitoring.UnitTest")]
namespace Netension.Monitoring.Prometheus.CustomMetrics
{

    internal class DurationMetric : IDurationMetric
    {
        private readonly ISummaryCollection _summaryCollection;
        private readonly Stopwatch _stopwatch;

        public string Name { get; }
        public string[] Labels { get; }

        public DurationMetric(ISummaryCollection summaryCollection, string name, params string[] labels)
        {
            _summaryCollection = summaryCollection;
            Name = name;
            Labels = labels;
            _stopwatch = Stopwatch.StartNew();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            _stopwatch.Stop();
            _summaryCollection.Observe(Name, _stopwatch.Elapsed.TotalMilliseconds, Labels);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
