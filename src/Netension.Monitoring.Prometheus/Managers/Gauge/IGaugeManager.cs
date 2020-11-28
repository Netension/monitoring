using Prometheus;
using System;

namespace Netension.Monitoring.Prometheus
{
    /// <summary>
    /// Manage <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metrics.
    /// </summary>
    public interface IGaugeManager
    {
        /// <summary>
        /// Get <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> from the collection.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see>.</param>
        /// <returns>Defintion of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.</returns>
        ///  <exception cref="InvalidOperationException">Throws, if <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> does not exist with given key.</exception>
        Gauge this[string name] { get; }

        /// <summary>
        /// Increases the value of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric with 1.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.</param>
        /// <param name="labels">Labels of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.</param>
        ///  <exception cref="InvalidOperationException">Throws, if <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> does not exist with given key.</exception>
        void Increase(string name, params string[] labels);
        /// <summary>
        /// Increases the value of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric with the given value.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.</param>
        /// <param name="increment">Value of the increment.</param>
        /// <param name="labels">Labels of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.</param>
        ///  <exception cref="InvalidOperationException">Throws, if <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> does not exist with given key.</exception>
        void Increase(string name, double increment, params string[] labels);
        /// <summary>
        /// Decreases the value of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric with 1.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see></param>
        /// <param name="labels">Labels of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.</param>
        ///  <exception cref="InvalidOperationException">Throws, if <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> does not exist with given key.</exception>
        void Decrease(string name, params string[] labels);
        /// <summary>
        /// Decreases the value of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric with the given value.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.</param>
        /// <param name="decrement">Value of decrement</param>
        /// <param name="labels">Labels of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.</param>
        ///  <exception cref="InvalidOperationException">Throws, if <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> does not exist with given key.</exception>
        void Decrease(string name, double decrement, params string[] labels);
        /// <summary>
        /// Set the value of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric to the given value.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.</param>
        /// <param name="value">Target value.</param>
        /// <param name="labels">Labels of the <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> metric.</param>
        ///  <exception cref="InvalidOperationException">Throws, if <see href="https://prometheus.io/docs/concepts/metric_types/#gauge">Gauge</see> does not exist with given key.</exception>
        void Set(string name, double value, params string[] labels);
    }
}
