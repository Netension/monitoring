using Prometheus;
using System;

namespace Netension.Monitoring.Prometheus
{
    /// <summary>
    /// Manage <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metrics.
    /// </summary>
    public interface ICounterManager
    {
        /// <summary>
        /// Get <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> from the collection.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see>.</param>
        /// <returns>Defintion of the <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric.</returns>
        ///  <exception cref="InvalidOperationException">Throws, if <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> does not exist with given key.</exception>
        Counter this[string name] { get; }
        /// <summary>
        /// Increase the value of a <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric with 1.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric.</param>
        /// <param name="labels">Labels of the <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric.</param>
        ///  <exception cref="InvalidOperationException">Throws, if <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> does not exist with given key.</exception>
        void Increase(string name, params string[] labels);
        /// <summary>
        /// Increases the value of a <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric with the given value.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric.</param>
        /// <param name="increment">Increment of the <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric.</param>
        /// <param name="labels">Labels of the <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric.</param>
        ///  <exception cref="InvalidOperationException">Throws, if <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> does not exist with given key.</exception>
        void Increase(string name, double increment, params string[] labels);
        /// <summary>
        /// Sets the value of a <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> to the given value.
        /// </summary>
        /// <param name="name">Name of the <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric.</param>
        /// <param name="value">Target value of the <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric.</param>
        /// <param name="labels">Labels of the <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> metric.</param>
        ///  <exception cref="InvalidOperationException">Throws, if <see href="https://prometheus.io/docs/concepts/metric_types/#counter">Counter</see> does not exist with given key.</exception>
        void Set(string name, double value, params string[] labels);
    }
}
