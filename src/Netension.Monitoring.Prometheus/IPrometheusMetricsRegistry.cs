using Microsoft.Extensions.Logging;
using Prometheus;
using System.Collections.Generic;

namespace Netension.Monitoring.Prometheus
{
    /// <summary>
    /// Makes possible to register new metrics.
    /// Supported metric types:
    /// <list type="bullet">
    /// <item>
    ///     <term><see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see></term>
    ///     <description>A counter is a cumulative metric that represents a single monotonically increasing counter whose value can only increase or be reset to zero on restart.</description>
    /// </item>
    /// <item>
    ///     <term><see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see></term>
    ///     <description>A gauge is a metric that represents a single numerical value that can arbitrarily go up and down.</description>
    /// </item>
    /// <item>
    ///     <term><see href="https://prometheus.io/docs/concepts/metric_types/#histogram">Histogram</see></term>
    ///     <description>A histogram samples observations (usually things like request durations or response sizes) and counts them in configurable buckets. It also provides a sum of all observed values.</description>
    /// </item>
    /// <item>
    ///     <term><see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see></term>
    ///     <description>Similar to a <see href="https://prometheus.io/docs/concepts/metric_types/#histogram">Histogram</see>, a <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> samples observations (usually things like request durations and response sizes).</description>
    /// </item>
    /// </list>
    /// </summary>
    public interface IPrometheusMetricsRegistry
    {
        /// <summary>
        /// Registrate a <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric.
        /// </summary>
        /// <param name="counter"><see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric definition.</param>
        IPrometheusMetricsRegistry RegistrateCounter(Counter counter);
        /// <summary>
        /// Create and registrate a <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric.
        /// </summary>
        /// <param name="name">Name of the metric.</param>
        /// <param name="description">Description of the metric.</param>
        IPrometheusMetricsRegistry RegistrateCounter(string name, string description);
        /// <summary>
        /// Create and registrate a <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric with labels.
        /// </summary>
        /// <param name="name">Name of the metric.</param>
        /// <param name="description">Description of the metric.</param>
        /// <param name="labels">Labels of the metric.</param>
        IPrometheusMetricsRegistry RegistrateCounter(string name, string description, IEnumerable<string> labels);

        /// <summary>
        /// Registrate a <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.
        /// </summary>
        /// <param name="gauge"><see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric definition.</param>
        IPrometheusMetricsRegistry RegistrateGauge(Gauge gauge);

        /// <summary>
        /// Registrate a <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.
        /// </summary>
        /// <param name="name">Name of the metric.</param>
        /// <param name="description">Descripton of the metric.</param>
        IPrometheusMetricsRegistry RegistrateGauge(string name, string description);

        /// <summary>
        /// Registrate a <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.
        /// </summary>
        /// <param name="name">Name of the metric.</param>
        /// <param name="description">Descripton of the metric.</param>
        /// <param name="labels">Labels of the metric.</param>
        IPrometheusMetricsRegistry RegistrateGauge(string name, string description, IEnumerable<string> labels);

        /// <summary>
        /// Registrate a <see href="https://prometheus.io/docs/concepts/metric_types/#histogram">Histogram</see> metric
        /// </summary>
        /// <param name="histogram"><see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Histogram</see> metric definition.</param>
        IPrometheusMetricsRegistry RegistrateHistogram(Histogram histogram);
        /// <summary>
        /// Create and register <see href="https://prometheus.io/docs/concepts/metric_types/#histogram">Histogram</see> metric.
        /// </summary>
        /// <param name="name">Name of the metric.</param>
        /// <param name="description">Description of the metric.</param>
        /// <param name="buckets">Buckets of the metric.</param>
        IPrometheusMetricsRegistry RegistrateHistogram(string name, string description, IEnumerable<double> buckets);
        /// <summary>
        /// Create and registrate <see href="https://prometheus.io/docs/concepts/metric_types/#histogram">Histogram</see> metric.
        /// </summary>
        /// <param name="name">Name of the metric.</param>
        /// <param name="description">Description of the metric.</param>
        /// <param name="buckets">Buckets of the metric.</param>
        /// <param name="labels">Labels of the metric.</param>
        IPrometheusMetricsRegistry RegistrateHistogram(string name, string description, IEnumerable<double> buckets, IEnumerable<string> labels);

        /// <summary>
        /// Register <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric.
        /// </summary>
        /// <param name="summary"><see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric definition.</param>
        IPrometheusMetricsRegistry RegistrateSummary(Summary summary);
        /// <summary>
        /// Create and registrate <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric.
        /// </summary>
        /// <param name="name">Name of the metric.</param>
        /// <param name="description">Description of the metric.</param>
        IPrometheusMetricsRegistry RegistrateSummary(string name, string description);
        /// <summary>
        /// Create and registrate <see href="https://prometheus.io/docs/concepts/metric_types/#summary">Summary</see> metric.
        /// </summary>
        /// <param name="name">Name of the metric.</param>
        /// <param name="description">Description of the metric.</param>
        /// <param name="labels">Labels of the metric.</param>
        IPrometheusMetricsRegistry RegistrateSummary(string name, string description, IEnumerable<string> labels);
    }
}
