using Microsoft.Extensions.Logging;
using Netension.Monitoring.Prometheus;
using Netension.Monitoring.Prometheus.Containers;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Netension.Monitoring.UnitTest.Prometheus
{
    public class PrometheusMetricsCollectionTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public PrometheusMetricsCollectionTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        private PrometheusMetricsCollection CreateSUT()
        {
            return new PrometheusMetricsCollection(new LoggerFactory().AddXUnit(_outputHelper));
        }

        #region Counter
        [Fact(DisplayName = "Register Counter multiple times")]
        public void PrometheusMetricsCollection_RegisterCounter_MultipleTimes()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateCounter(namesGenerator.GetRandomName(), namesGenerator.GetRandomName(), new string[] { namesGenerator.GetRandomName() });

            sut.RegisterCounter(metric);

            // Act
            sut.RegisterCounter(metric);

            // Assert - Did not throw exception
            Assert.True(true);
        }

        [Fact(DisplayName = "Register Counter with definition")]
        public void PrometheusMetricsCollection_RegisterCounter_Definition()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateCounter(namesGenerator.GetRandomName(), namesGenerator.GetRandomName(), new string[] { namesGenerator.GetRandomName() });

            // Act
            sut.RegisterCounter(metric);

            // Assert
            Assert.Equal(metric, ((ICounterCollection)sut)[metric.Name]);
        }

        [Fact(DisplayName = "Register Counter with name")]
        public void PrometheusMetricsCollection_RegisterCounter_Name()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var description = namesGenerator.GetRandomName();

            // Act
            sut.RegisterCounter(name, description);

            // Assert
            var metric = ((ICounterCollection)sut)[name];
            Assert.Equal(name, metric.Name);
            Assert.Equal(description, metric.Help);
            Assert.Empty(metric.LabelNames);
        }

        [Fact(DisplayName = "Register Counter with labels")]
        public void PrometheusMetricsCollection_RegisterCounter_Labels()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var description = namesGenerator.GetRandomName();
            var labels = new List<string> { namesGenerator.GetRandomName() };

            // Act
            sut.RegisterCounter(name, description, labels);

            // Assert
            var metric = ((ICounterCollection)sut)[name];
            Assert.Equal(name, metric.Name);
            Assert.Equal(description, metric.Help);
            Assert.Equal(labels, metric.LabelNames);
        }

        [Fact(DisplayName = "Get Counter")]
        public void PrometheusMetricsCollection_GetCounter()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (ICounterCollection)collection;
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var description = namesGenerator.GetRandomName();

            collection.RegisterCounter(name, description);

            // Act
            var metric = sut[name];

            // Assert
            Assert.NotNull(metric);
        }

        [Fact(DisplayName = "Get not existing Counter")]
        public void PrometheusMetricsCollection_GetNotExistingCounter()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (ICounterCollection)collection;

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => sut[new NamesGenerator().GetRandomName()]);
        }

        [Fact(DisplayName = "Increase Counter with increment")]
        public void PrometheusMetricsCollection_IncreaseCounterWithIncrement()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (ICounterCollection)collection;
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var description = namesGenerator.GetRandomName();

            var increment = 5.0;

            collection.RegisterCounter(name, description);

            // Act
            sut.Increase(name, increment);

            // Assert
            Assert.Equal(increment, sut[name].Value);
        }

        [Fact(DisplayName = "Set Counter")]
        public void PrometheusMetricsCollection_SetCounter()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (ICounterCollection)collection;
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var description = namesGenerator.GetRandomName();

            var value = 5.0;

            collection.RegisterCounter(name, description);

            // Act
            sut.Set(name, value);

            // Assert
            Assert.Equal(value, sut[name].Value);
        }
        #endregion

        #region Gauge
        [Fact(DisplayName = "Register gauge muliptle times")]
        public void PrometheusMetricsCollection_RegisterGauge_MultipleTimes()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateGauge(namesGenerator.GetRandomName(), namesGenerator.GetRandomName(), new string[] { namesGenerator.GetRandomName() });

            sut.RegisterGauge(metric);

            // Act
            sut.RegisterGauge(metric);

            // Assert - Did not throw exception
            Assert.True(true);
        }

        [Fact(DisplayName = "Register gauge with definition")]
        public void PrometheusMetricsCollection_RegisterGauge_Definition()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateGauge(namesGenerator.GetRandomName(), namesGenerator.GetRandomName(), new string[] { namesGenerator.GetRandomName() });

            // Act
            sut.RegisterGauge(metric);

            // Assert
            Assert.Equal(metric, ((IGaugeCollection)sut)[metric.Name]);
        }

        [Fact(DisplayName = "Register Gauge with name")]
        public void PrometheusMetricsCollection_RegisterGauge_Name()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var description = namesGenerator.GetRandomName();

            // Act
            sut.RegisterGauge(name, description);

            // Assert
            var metric = ((IGaugeCollection)sut)[name];
            Assert.Equal(name, metric.Name);
            Assert.Equal(description, metric.Help);
            Assert.Empty(metric.LabelNames);
        }

        [Fact(DisplayName = "Register counter with labels")]
        public void PrometheusMetricsCollection_RegisterGauge_Labels()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var description = namesGenerator.GetRandomName();
            var labels = new List<string> { namesGenerator.GetRandomName() };

            // Act
            sut.RegisterGauge(name, description, labels);

            // Assert
            var metric = ((IGaugeCollection)sut)[name];
            Assert.Equal(name, metric.Name);
            Assert.Equal(description, metric.Help);
            Assert.Equal(labels, metric.LabelNames);
        }


        [Fact(DisplayName = "Get Gauge")]
        public void PrometheusMetricsCollection_GetGauge()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (IGaugeCollection)collection;
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateGauge(namesGenerator.GetRandomName(), namesGenerator.GetRandomName());

            collection.RegisterGauge(metric);

            // Act
            var result = sut[metric.Name];

            // Assert
            Assert.Equal(metric, result);
        }


        [Fact(DisplayName = "Get not existing Gauge")]
        public void PrometheusMetricsCollection_GetNotExistingGauge()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (IGaugeCollection)collection;

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => sut[new NamesGenerator().GetRandomName()]);
        }

        [Fact(DisplayName = "Increase Gauge")]
        public void PrometheusMetricsCollection_IncreaseGauge()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (IGaugeCollection)collection;
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateGauge(namesGenerator.GetRandomName(), namesGenerator.GetRandomName());

            collection.RegisterGauge(metric);

            // Act
            sut.Increase(metric.Name);

            // Assert
            Assert.Equal(1.0, sut[metric.Name].Value);
        }


        [Fact(DisplayName = "Increase Gauge with increment")]
        public void PrometheusMetricsCollection_IncreaseGaugeWithIncrement()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (IGaugeCollection)collection;
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateGauge(namesGenerator.GetRandomName(), namesGenerator.GetRandomName());

            collection.RegisterGauge(metric);

            var increment = 5.0;

            // Act
            sut.Increase(metric.Name, increment);

            // Assert
            Assert.Equal(increment, sut[metric.Name].Value);
        }

        [Fact(DisplayName = "Decrease Gauge")]
        public void PrometheusMetricsCollection_DecreaseGauge()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (IGaugeCollection)collection;
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateGauge(namesGenerator.GetRandomName(), namesGenerator.GetRandomName());

            collection.RegisterGauge(metric);

            // Act
            sut.Decrease(metric.Name);

            // Assert
            Assert.Equal(-1.0, sut[metric.Name].Value);
        }

        [Fact(DisplayName = "Decrease Gauge with decrement")]
        public void PrometheusMetricsCollection_DecreaseGaugeWithDecrement()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (IGaugeCollection)collection;
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateGauge(namesGenerator.GetRandomName(), namesGenerator.GetRandomName());

            collection.RegisterGauge(metric);

            var decremant = 5.0;

            // Act
            sut.Decrease(metric.Name, decremant);

            // Assert
            Assert.Equal(decremant * -1, sut[metric.Name].Value);
        }

        [Theory(DisplayName = "Decrease Gauge with decrement")]
        [InlineData(5.0)]
        [InlineData(-5.0)]
        public void PrometheusMetricsCollection_SetGauge(double value)
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (IGaugeCollection)collection;
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateGauge(namesGenerator.GetRandomName(), namesGenerator.GetRandomName());

            collection.RegisterGauge(metric);

            // Act
            sut.Set(metric.Name, value);

            // Assert
            Assert.Equal(value, sut[metric.Name].Value);
        }
        #endregion

        #region Histogram
        [Fact(DisplayName = "Register Histogram multiple times")]
        public void PrometheusMetricsCollection_RegisterHistogram_MultipleTimes()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateHistogram(namesGenerator.GetRandomName(), namesGenerator.GetRandomName(), new HistogramConfiguration { Buckets = new double[] { 1.0, 2.0 }, LabelNames = new string[] { namesGenerator.GetRandomName() } });

            sut.RegisterHistogram(metric);

            // Act
            sut.RegisterHistogram(metric);

            // Assert - Did not throw exception
            Assert.True(true);
        }

        [Fact(DisplayName = "Register Histogram with definition")]
        public void PrometheusMetricsCollection_RegisterHistogram_Definition()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateHistogram(namesGenerator.GetRandomName(), namesGenerator.GetRandomName(), new HistogramConfiguration { Buckets = new double[] { 1.0, 2.0 }, LabelNames = new string[] { namesGenerator.GetRandomName() } });

            // Act
            sut.RegisterHistogram(metric);

            // Assert
            Assert.Equal(metric, ((IHistogramCollection)sut)[metric.Name]);
        }

        [Fact(DisplayName = "Register Histogram with name")]
        public void PrometheusMetricsCollection_RegisterHistogram_Name()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var description = namesGenerator.GetRandomName();
            var buckets = new List<double> { 1.0, 2.0 };

            // Act
            sut.RegisterHistogram(name, description, buckets);

            // Assert
            var metric = ((IHistogramCollection)sut)[name];
            Assert.Equal(name, metric.Name);
            Assert.Equal(description, metric.Help);
            Assert.Empty(metric.LabelNames);
        }

        [Fact(DisplayName = "Register Histogram with labels")]
        public void PrometheusMetricsCollection_RegisterHistogram_Labels()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var description = namesGenerator.GetRandomName();
            var buckets = new List<double> { 1.0, 2.0 };
            var labels = new List<string> { namesGenerator.GetRandomName() };

            // Act
            sut.RegisterHistogram(name, description, buckets, labels);

            // Assert
            var metric = ((IHistogramCollection)sut)[name];
            Assert.Equal(name, metric.Name);
            Assert.Equal(description, metric.Help);
            Assert.Equal(labels, metric.LabelNames);
        }

        [Fact(DisplayName = "Get Histogram")]
        public void PrometheusMetricsCollection_GetHistogram()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (IHistogramCollection)collection;
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateHistogram(namesGenerator.GetRandomName(), namesGenerator.GetRandomName(), new HistogramConfiguration { Buckets = new double[] { 1.0, 2.0 }, LabelNames = new string[] { namesGenerator.GetRandomName() } });

            collection.RegisterHistogram(metric);

            // Act
            // Assert
            Assert.Equal(metric, sut[metric.Name]);
        }

        [Fact(DisplayName = "Get not existing Histogram")]
        public void PrometheusMetricsCollection_GetNotExistingHistogram()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (IHistogramCollection)collection;

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => sut[new NamesGenerator().GetRandomName()]);
        }

        [Fact(DisplayName = "Observe Histogram")]
        public void PrometheusMetricsCollection_ObserveHistogram()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (IHistogramCollection)collection;
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateHistogram(namesGenerator.GetRandomName(), namesGenerator.GetRandomName(), new HistogramConfiguration { Buckets = new double[] { 1.0, 2.0 } });

            collection.RegisterHistogram(metric);

            var value = 1.5;

            // Act
            sut.Observe(metric.Name, value);

            // Assert
            Assert.Equal(value, sut[metric.Name].Sum);
        }
        #endregion

        #region Summary
        [Fact(DisplayName = "Register Summary multiple times")]
        public void PrometheusMetricsCollection_RegisterSummary_MultipleTimes()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateSummary(namesGenerator.GetRandomName(), namesGenerator.GetRandomName(), new string[] { namesGenerator.GetRandomName() });

            sut.RegisterSummary(metric);

            // Act
            sut.RegisterSummary(metric);

            // Assert - Did not throw exception
            Assert.True(true);
        }

        [Fact(DisplayName = "Register Summary with definition")]
        public void PrometheusMetricsCollection_RegisterSummary_Definition()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateSummary(namesGenerator.GetRandomName(), namesGenerator.GetRandomName(), new string[] { namesGenerator.GetRandomName() });

            // Act
            sut.RegisterSummary(metric);

            // Assert
            Assert.Equal(metric, ((ISummaryCollection)sut)[metric.Name]);
        }

        [Fact(DisplayName = "Register Summary with name")]
        public void PrometheusMetricsCollection_RegisterSummary_Name()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var description = namesGenerator.GetRandomName();

            // Act
            sut.RegisterSummary(name, description);

            // Assert
            var metric = ((ISummaryCollection)sut)[name];
            Assert.Equal(name, metric.Name);
            Assert.Equal(description, metric.Help);
            Assert.Empty(metric.LabelNames);
        }

        [Fact(DisplayName = "Register Summary with labels")]
        public void PrometheusMetricsCollection_RegisterSummary_Labels()
        {
            // Arrange
            var sut = CreateSUT();
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var description = namesGenerator.GetRandomName();
            var labels = new List<string> { namesGenerator.GetRandomName() };

            // Act
            sut.RegisterSummary(name, description, labels);

            // Assert
            var metric = ((ISummaryCollection)sut)[name];
            Assert.Equal(name, metric.Name);
            Assert.Equal(description, metric.Help);
            Assert.Equal(labels, metric.LabelNames);
        }

        [Fact(DisplayName = "Get Summary")]
        public void PrometheusMetricsCollection_GetSummary()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (ISummaryCollection)collection;
            var namesGenerator = new NamesGenerator();
            var metric = Metrics.CreateSummary(namesGenerator.GetRandomName(), namesGenerator.GetRandomName());

            collection.RegisterSummary(metric);

            // Act
            // Assert
            Assert.Equal(metric, sut[metric.Name]);
        }

        [Fact(DisplayName = "Get not existing Summary")]
        public void PrometheusMetricsCollection_GetNotExistingSummary()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (ISummaryCollection)collection;

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => sut[new NamesGenerator().GetRandomName()]);
        }

        [Fact]
        public void PrometheusMetricsCollection_StartDurationMeasure()
        {
            // Arrange
            var collection = CreateSUT();
            var sut = (ISummaryCollection)collection;
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var labels = new string[] { namesGenerator.GetRandomName() };

            collection.RegisterSummary(name, string.Empty);

            // Act
            var result = sut.StartDurationMeasurement(name, labels);

            // Assert
            Assert.Equal(name, result.Name);
            Assert.Equal(labels, result.Labels);
        }
        #endregion
    }
}
