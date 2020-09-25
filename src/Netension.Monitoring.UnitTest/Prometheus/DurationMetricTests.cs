using Moq;
using Netension.Monitoring.Prometheus;
using System.Text;
using Xunit;

namespace Netension.Monitoring.UnitTest.Prometheus
{
    public class DurationMetricTests
    {
        private Mock<ISummaryCollection> _summaryCollectionMock;

        private DurationMetric CreateSUT(string name, params string[] labels)
        {
            _summaryCollectionMock = new Mock<ISummaryCollection>();

            return new DurationMetric(_summaryCollectionMock.Object, name, labels);
        }

        [Fact(DisplayName = "Duration metric observe elapsed time")]
        public void DurationMetric_Dispose_CallObserve()
        {
            // Arrange
            var namesGenerator = new NamesGenerator();
            var name = namesGenerator.GetRandomName();
            var label = namesGenerator.GetRandomName();

            var sut = CreateSUT(name, label);

            // Act
            sut.Dispose();

            // Assert
            _summaryCollectionMock.Verify(sc => sc.Observe(It.Is<string>(n => n.Equals(name)), It.Is<double>(et => et > 0), It.Is<string[]>(l => l[0] == label)), Times.Once);
        }
    }
}
