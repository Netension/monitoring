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
    public class HistogramManagerTests
    {
        private NamesGenerator _namesGenerator = new NamesGenerator();
        private readonly ITestOutputHelper _outputHelper;
        private PrometheusMetricsCollection _collection;

        public HistogramManagerTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        private HistogramManager CreateSUT()
        {
            _collection = new PrometheusMetricsCollection();
            var loggerFactory = new LoggerFactory()
                                    .AddXUnit(_outputHelper);

            return new HistogramManager(_collection, loggerFactory);
        }

        private string RegistrateHistogram()
        {
            var name = _namesGenerator.GetRandomName();
            _collection.Add(new MetricDefinition<Histogram>(name, Metrics.CreateHistogram(name, _namesGenerator.GetRandomName())));
            return name;
        }

        [Fact]
        public void HistogramManager_Observe()
        {
            // Arrange
            var sut = CreateSUT();
            var metric = RegistrateHistogram();

            // Act
            sut.Observe(metric, 5.0);

            // Assert
            Assert.Equal(1, _collection.GetHistogram(metric).Metric.Count);
        }

        [Fact]
        public void HistogramManager_Observe_NotExistsMetric()
        {
            // Arrange
            var sut = CreateSUT();

            // Act
            sut.Observe(_namesGenerator.GetRandomName(), 5.0);

            // Assert - Did not throw exception
            Assert.True(true);
        }
    }
}
