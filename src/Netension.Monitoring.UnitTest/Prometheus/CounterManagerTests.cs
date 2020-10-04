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
    public class CounterManagerTests
    {
        private readonly NamesGenerator _namesGenerator = new NamesGenerator();
        private readonly ITestOutputHelper _outputHelper;
        private PrometheusMetricsCollection _collection;
        private ILogger<CounterManagerTests> _logger;

        public CounterManagerTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        private CounterManager CreateSUT()
        {
            _collection = new PrometheusMetricsCollection();
            var loggerFactory = new LoggerFactory()
                .AddXUnit(_outputHelper);

            _logger = loggerFactory.CreateLogger<CounterManagerTests>();

            return new CounterManager(_collection, loggerFactory);
        }

        private string RegistrateCounter()
        {
            var name = _namesGenerator.GetRandomName();

            _collection.Add(new MetricDefinition<Counter>(name, Metrics.CreateCounter(name, _namesGenerator.GetRandomName())));

            return name;
        }

        [Fact]
        public void CounterManager_Increase_DefaultIncrement()
        {
            // Arrange
            var sut = CreateSUT();
            var name = RegistrateCounter();

            // Act
            sut.Increase(name);

            // Assert
            Assert.Equal(1.0, _collection.GetCounter(name).Metric.Value);
        }

        [Fact]
        public void CounterManager_Increase_SetIncrement()
        {
            // Arrange
            var sut = CreateSUT();
            var name = RegistrateCounter();

            // Act
            sut.Increase(name, 5.0);

            // Assert
            Assert.Equal(5.0, _collection.GetCounter(name).Metric.Value);
        }

        [Fact]
        public void CounterManager_Increase_NotExistsMetric()
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            sut.Increase(_namesGenerator.GetRandomName());

            // Assert - Did not thrown exception
            Assert.True(true);
        }

        [Fact]
        public void CounterManager_Set()
        {
            // Arrange
            var sut = CreateSUT();
            var name = RegistrateCounter();

            // Act
            sut.Set(name, 5.0);

            // Assert - Did not thrown exception
            Assert.Equal(5.0, _collection.GetCounter(name).Metric.Value);
        }

        [Fact]
        public void CounterManager_Set_NotExistsMetric()
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            sut.Set(_namesGenerator.GetRandomName(), 5.0);

            // Assert - Did not thrown exception
            Assert.True(true);
        }
    }
}
