using StiebelEltronDashboard.Extensions;
using System;
using Xunit;

namespace StiebelEltronDashboardTests
{
    public class DateTimeTests
    {
        [Fact]
        public void WhenToEpochCorrectUnixTimeIsReturned()
        {
            var dateTime = new DateTime(2020, 2, 1);
            // Act
            var result = dateTime.ToEpoch();
            // Assert
            Assert.Equal(1580515200000, result);
        }
    }
}
