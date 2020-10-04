using Netension.Monitoring.Prometheus.Collections;
using Prometheus;
using System;

namespace Netension.Monitoring.Prometheus.Managers
{
    internal class GaugeManager : IGaugeManager
    {
        private readonly PrometheusMetricsCollection _metrics;

        public GaugeManager(PrometheusMetricsCollection metrics)
        {
            _metrics = metrics;
        }

        public Gauge this[string name] { get { return _metrics.GetGauge(name).Metric; } }

        public void Decrease(string name, params string[] labels)
        {
            throw new NotImplementedException();
        }

        public void Decrease(string name, double decrement, params string[] labels)
        {
            throw new NotImplementedException();
        }

        public void Increase(string name, params string[] labels)
        {
            throw new NotImplementedException();
        }

        public void Increase(string name, double increment, params string[] labels)
        {
            throw new NotImplementedException();
        }

        public void Set(string name, double value, params string[] labels)
        {
            throw new NotImplementedException();
        }
    }
}
