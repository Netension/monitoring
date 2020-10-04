using Microsoft.Extensions.Logging;
using Netension.Monitoring.Core.Models;
using Netension.Monitoring.Prometheus;
using Netension.Monitoring.Prometheus.Collections;
using Prometheus;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Netension.Monitoring.UnitTest.Prometheus
{
    public class PrometheusMetricsRegistryTests
    {
        private readonly NamesGenerator _namesGenerator = new NamesGenerator();
        private readonly ITestOutputHelper _outputHelper;
        private PrometheusMetricsCollection _collection;

        public PrometheusMetricsRegistryTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        private PrometheusMetricsRegistry CreateSUT()
        {
            _collection = new PrometheusMetricsCollection();

            return new PrometheusMetricsRegistry(_collection, new LoggerFactory().AddXUnit(_outputHelper));
        }

        #region Counter
        [Fact]
        public void PrometheusMetricsRegistry_RegistrateCounter_RegistrateDefinition()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = Metrics.CreateCounter(_namesGenerator.GetRandomName(), _namesGenerator.GetRandomName(), new string[] { _namesGenerator.GetRandomName() });

            // Act
            sut.RegistrateCounter(metric);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Counter>>)_collection, m => m.Key.Equals(metric.Name) && m.Metric == metric);
        }

        [Fact]
        public void PrometheusMetricsRegistry_RegistrateCounter_RegistrateWithoutLabels()
        {
            // Arrange
            var sut = CreateSUT();
            var name = _namesGenerator.GetRandomName();
            var description = _namesGenerator.GetRandomName();

            // Act
            sut.RegistrateCounter(name, description);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Counter>>)_collection, m => m.Key.Equals(name));
        }

        [Fact]
        public void PrometheusMetricsRegistry_RegistrateCounter_RegistrateWithLabels()
        {
            // Arrange
            var sut = CreateSUT();
            var name = _namesGenerator.GetRandomName();
            var description = _namesGenerator.GetRandomName();
            var labels = new string[] { _namesGenerator.GetRandomName() };

            // Act
            sut.RegistrateCounter(name, description, labels);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Counter>>)_collection, m => m.Key.Equals(name));
        }

        [Fact]
        public void PrometheusMetricsRegistry_RegistrateCounter_DoubledRegistration()
        {
            // Arrange
            var sut = CreateSUT();
            var name = _namesGenerator.GetRandomName();
            var description = _namesGenerator.GetRandomName();
            var labels = new string[] { _namesGenerator.GetRandomName() };

            var metric = Metrics.CreateCounter(name, _namesGenerator.GetRandomName(), new string[] { _namesGenerator.GetRandomName() });
            sut.RegistrateCounter(metric);

            // Act
            sut.RegistrateCounter(name, description, labels);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Counter>>)_collection, m => m.Key.Equals(metric.Name) && m.Metric == metric);
            Assert.DoesNotContain((IEnumerable<MetricDefinition<Counter>>)_collection, m => m.Metric.Help == description && m.Metric.LabelNames == labels);
        }
        #endregion

        #region Gauge
        [Fact]
        public void PrometheusMetricsRegistry_RegistrateGauge_RegistrateDefinition()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = Metrics.CreateGauge(_namesGenerator.GetRandomName(), _namesGenerator.GetRandomName(), new string[] { _namesGenerator.GetRandomName() });

            // Act
            sut.RegistrateGauge(metric);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Gauge>>)_collection, m => m.Key.Equals(metric.Name) && m.Metric == metric);
        }

        [Fact]
        public void PrometheusMetricsRegistry_RegistrateGauge_RegistrateWithoutLabels()
        {
            // Arrange
            var sut = CreateSUT();
            var name = _namesGenerator.GetRandomName();
            var description = _namesGenerator.GetRandomName();

            // Act
            sut.RegistrateGauge(name, description);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Gauge>>)_collection, m => m.Key.Equals(name));
        }

        [Fact]
        public void PrometheusMetricsRegistry_RegistrateGauge_RegistrateWithLabels()
        {
            // Arrange
            var sut = CreateSUT();
            var name = _namesGenerator.GetRandomName();
            var description = _namesGenerator.GetRandomName();
            var labels = new string[] { _namesGenerator.GetRandomName() };

            // Act
            sut.RegistrateGauge(name, description, labels);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Gauge>>)_collection, m => m.Key.Equals(name));
        }

        [Fact]
        public void PrometheusMetricsRegistry_RegistrateGauge_DoubledRegistration()
        {
            // Arrange
            var sut = CreateSUT();
            var name = _namesGenerator.GetRandomName();
            var description = _namesGenerator.GetRandomName();
            var labels = new string[] { _namesGenerator.GetRandomName() };

            var metric = Metrics.CreateGauge(name, _namesGenerator.GetRandomName(), new string[] { _namesGenerator.GetRandomName() });
            sut.RegistrateGauge(metric);

            // Act
            sut.RegistrateGauge(name, description, labels);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Gauge>>)_collection, m => m.Key.Equals(metric.Name) && m.Metric == metric);
            Assert.DoesNotContain((IEnumerable<MetricDefinition<Gauge>>)_collection, m => m.Metric.Help == description && m.Metric.LabelNames == labels);
        }
        #endregion

        #region Histogram
        [Fact]
        public void PrometheusMetricsRegistry_RegistrateHistogram_RegistrateDefinition()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = Metrics.CreateHistogram(_namesGenerator.GetRandomName(), _namesGenerator.GetRandomName(), new string[] { _namesGenerator.GetRandomName() });

            // Act
            sut.RegistrateHistogram(metric);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Histogram>>)_collection, m => m.Key.Equals(metric.Name) && m.Metric == metric);
        }

        [Fact]
        public void PrometheusMetricsRegistry_RegistrateHistogram_RegistrateWithoutLabels()
        {
            // Arrange
            var sut = CreateSUT();
            var name = _namesGenerator.GetRandomName();
            var description = _namesGenerator.GetRandomName();
            var buckets = new List<double> { 1.0, 2.0 };

            // Act
            sut.RegistrateHistogram(name, description, buckets);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Histogram>>)_collection, m => m.Key.Equals(name));
        }

        [Fact]
        public void PrometheusMetricsRegistry_RegistrateHistogram_RegistrateWithLabels()
        {
            // Arrange
            var sut = CreateSUT();
            var name = _namesGenerator.GetRandomName();
            var description = _namesGenerator.GetRandomName();
            var buckets = new List<double> { 1.0, 2.0 };
            var labels = new string[] { _namesGenerator.GetRandomName() };

            // Act
            sut.RegistrateHistogram(name, description, buckets, labels);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Histogram>>)_collection, m => m.Key.Equals(name));
        }

        [Fact]
        public void PrometheusMetricsRegistry_RegistrateHistogram_DoubledRegistration()
        {
            // Arrange
            var sut = CreateSUT();
            var name = _namesGenerator.GetRandomName();
            var description = _namesGenerator.GetRandomName();
            var buckets = new List<double> { 1.0, 2.0 };
            var labels = new string[] { _namesGenerator.GetRandomName() };

            var metric = Metrics.CreateHistogram(name, _namesGenerator.GetRandomName(), new string[] { _namesGenerator.GetRandomName() });
            sut.RegistrateHistogram(metric);

            // Act
            sut.RegistrateHistogram(name, description, buckets, labels);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Histogram>>)_collection, m => m.Key.Equals(metric.Name) && m.Metric == metric);
            Assert.DoesNotContain((IEnumerable<MetricDefinition<Histogram>>)_collection, m => m.Metric.Help == description && m.Metric.LabelNames == labels);
        }

        #endregion

        #region Summary
        [Fact]
        public void PrometheusMetricsRegistry_RegistrateSummary_RegistrateDefinition()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = Metrics.CreateSummary(_namesGenerator.GetRandomName(), _namesGenerator.GetRandomName(), new string[] { _namesGenerator.GetRandomName() });

            // Act
            sut.RegistrateSummary(metric);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Summary>>)_collection, m => m.Key.Equals(metric.Name) && m.Metric == metric);
        }

        [Fact]
        public void PrometheusMetricsRegistry_RegistrateSummary_RegistrateWithoutLabels()
        {
            // Arrange
            var sut = CreateSUT();
            var name = _namesGenerator.GetRandomName();
            var description = _namesGenerator.GetRandomName();

            // Act
            sut.RegistrateSummary(name, description);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Summary>>)_collection, m => m.Key.Equals(name));
        }

        [Fact]
        public void PrometheusMetricsRegistry_RegistrateSummary_RegistrateWithLabels()
        {
            // Arrange
            var sut = CreateSUT();
            var name = _namesGenerator.GetRandomName();
            var description = _namesGenerator.GetRandomName();
            var labels = new string[] { _namesGenerator.GetRandomName() };

            // Act
            sut.RegistrateSummary(name, description, labels);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Summary>>)_collection, m => m.Key.Equals(name));
        }

        [Fact]
        public void PrometheusMetricsRegistry_RegistrateSummary_DoubledRegistration()
        {
            // Arrange
            var sut = CreateSUT();
            var name = _namesGenerator.GetRandomName();
            var description = _namesGenerator.GetRandomName();
            var labels = new string[] { _namesGenerator.GetRandomName() };

            var metric = Metrics.CreateSummary(name, _namesGenerator.GetRandomName(), new string[] { _namesGenerator.GetRandomName() });
            sut.RegistrateSummary(metric);

            // Act
            sut.RegistrateSummary(name, description, labels);

            // Assert
            Assert.Contains((IEnumerable<MetricDefinition<Summary>>)_collection, m => m.Key.Equals(metric.Name) && m.Metric == metric);
            Assert.DoesNotContain((IEnumerable<MetricDefinition<Summary>>)_collection, m => m.Metric.Help == description && m.Metric.LabelNames == labels);
        }
        #endregion
    }
}
