using Castle.Core;
using Microsoft.Extensions.Logging;
using Netension.Monitoring.Core.Models;
using Netension.Monitoring.Prometheus.Collections;
using Netension.Monitoring.Prometheus.Managers;
using Prometheus;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Netension.Monitoring.UnitTest.Prometheus
{
    public class GaugeManagerTests
    {
        private readonly NamesGenerator _namesGenerator = new NamesGenerator();
        private readonly ITestOutputHelper _outputHelper;
        private PrometheusMetricsCollection _collection;

        public GaugeManagerTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        private GaugeManager CreateSUT()
        {
            _collection = new PrometheusMetricsCollection();
            var loggerFactory = new LoggerFactory()
                .AddXUnit(_outputHelper);

            return new GaugeManager(_collection, loggerFactory);
        }

        private string RegistrateGauge()
        {
            var name = _namesGenerator.GetRandomName();
            _collection.Add(new MetricDefinition<Gauge>(name, Metrics.CreateGauge(name, _namesGenerator.GetRandomName())));
            return name;
        }

        [Fact]
        public void GaugeManager_Increase_DefaultValue()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = RegistrateGauge();

            // Act
            sut.Increase(metric);

            // Assert
            Assert.Equal(1.0, _collection.GetGauge(metric).Metric.Value);
        }

        [Fact]
        public void GaugeManager_Increase_SetIncrement()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = RegistrateGauge();

            // Act
            sut.Increase(metric, 5.0);

            // Assert
            Assert.Equal(5.0, _collection.GetGauge(metric).Metric.Value);
        }

        [Fact]
        public void GaugeManager_Increase_NotExistsMetric()
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            sut.Increase(_namesGenerator.GetRandomName());

            // Assert - Did not throw exception
            Assert.True(true);
        }

        [Fact]
        public void GaugeManager_Decrease_DefaultValue()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = RegistrateGauge();

            // Act
            sut.Decrease(metric);

            // Assert
            Assert.Equal(-1.0, _collection.GetGauge(metric).Metric.Value);
        }

        [Fact]
        public void GaugeManager_Decrease_SetIncrement()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = RegistrateGauge();

            // Act
            sut.Decrease(metric, 5.0);

            // Assert
            Assert.Equal(-5.0, _collection.GetGauge(metric).Metric.Value);
        }

        [Fact]
        public void GaugeManager_Decrease_NotExistsMetric()
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            sut.Decrease(_namesGenerator.GetRandomName());

            // Assert - Did not throw exception
            Assert.True(true);
        }

        [Fact]
        public void GaugeManager_Set_IncrementTo()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = RegistrateGauge();

            // Act
            sut.Set(metric, 5.0);

            // Assert
            Assert.Equal(5.0, _collection.GetGauge(metric).Metric.Value);
        }

        [Fact]
        public void GaugeManager_Set_DecrementTo()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = RegistrateGauge();

            // Act
            sut.Set(metric, -5.0);

            // Assert
            Assert.Equal(-5.0, _collection.GetGauge(metric).Metric.Value);
        }

        [Fact]
        public void GaugeManager_Set_NotExistsMetric()
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            sut.Set(_namesGenerator.GetRandomName(), 1.0);

            // Assert - Did not throw exception
            Assert.True(true);
        }
    }
}
