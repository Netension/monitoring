using Microsoft.Extensions.Logging;
using Netension.Monitoring.Core.Diagnostics;
using System;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Netension.Monitoring.UnitTest.Core
{
    public class StopwatchCollectionTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public StopwatchCollectionTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        private StopwatchCollection CreateSUT()
        {
            return new StopwatchCollection(new LoggerFactory().AddXUnit(_outputHelper));
        }

        [Fact(DisplayName = "Start new stopwatch")]
        public void StopwatchCollection_Start_AddNewStopwatch()
        {
            // Arrange
            var sut = CreateSUT();
            var key = new NamesGenerator().GetRandomName();

            // Act
            sut.Start(key);

            // Assert
            Assert.NotEqual(TimeSpan.Zero, sut[key]);
        }

        [Fact(DisplayName = "Stop not existings stopwatch")]
        public void StopwatchCollection_Stop_StopNotExistingStopwatch()
        {
            // Arrange
            var sut = CreateSUT();
            var key = new NamesGenerator().GetRandomName();

            // Act
            // Assert
            Assert.Equal(TimeSpan.Zero, sut[key]);
        }
    }
}
