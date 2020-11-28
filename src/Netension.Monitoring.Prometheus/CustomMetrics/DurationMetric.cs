using Prometheus;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Netension.Monitoring.UnitTest")]
namespace Netension.Monitoring.Prometheus.CustomMetrics
{
    [ExcludeFromCodeCoverage]
    internal class DurationMetric : IDurationMetric
    {
        private readonly Summary _summary;
        private readonly Stopwatch _stopwatch;

        public string Name { get; }
        public string[] Labels { get; }

        public DurationMetric(Summary summary, string name, params string[] labels)
        {
            _summary = summary;
            Name = name;
            Labels = labels;
            _stopwatch = Stopwatch.StartNew();
        }

        [ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            _stopwatch.Stop();
            _summary.WithLabels(Labels).Observe(_stopwatch.Elapsed.TotalMilliseconds);
        }

        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
