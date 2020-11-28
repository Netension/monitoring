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
    public class SummaryManagerTests
    {
        private readonly NamesGenerator _namesGenerator = new NamesGenerator();
        private readonly ITestOutputHelper _outputHelper;
        private PrometheusMetricsCollection _collection;

        public SummaryManagerTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        private SummaryManager CreateSUT()
        {
            _collection = new PrometheusMetricsCollection();
            var loggerFactory = new LoggerFactory()
                                    .AddXUnit(_outputHelper);

            return new SummaryManager(_collection, loggerFactory);
        }

        private string RegistrateSummary()
        {
            var name = _namesGenerator.GetRandomName();
            _collection.Add(new MetricDefinition<Summary>(name, Metrics.CreateSummary(name, _namesGenerator.GetRandomName())));
            return name;
        }

        [Fact]
        public void SummaryManager_Observe()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = RegistrateSummary();

            // Act
            sut.Observe(metric, 5.0);

            // Assert - Did not throw exception
            Assert.True(true);
        }

        [Fact]
        public void SummaryManager_Observe_NotExistsMetric()
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            sut.Observe(_namesGenerator.GetRandomName(), 5.0);

            // Assert - Did not throw exception
            Assert.True(true);
        }

        [Fact]
        public void SummaryManager_MeasureDuration()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = RegistrateSummary();

            // Act
            using var duration = sut.MeasureDuration(metric);

            // Assert - Did not throw exception
            Assert.Equal(metric, duration.Name);
        }

        [Fact]
        public void SummaryManager_MeasureDuration_NotExistsMetric()
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            sut.MeasureDuration(_namesGenerator.GetRandomName());

            // Assert - Did not throw exception
            Assert.True(true);
        }
    }
}
