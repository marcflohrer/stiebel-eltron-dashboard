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

        [Fact]
        public void WhenDateIsEnteredCalendarWeekIsReturned()
        {
            var year = 2021;
            var periodNumber = 22;
            var result = new DateTime(year, 1, 1).FirstDateOfWeek(periodNumber, new System.Globalization.CultureInfo("de-DE"));

            Assert.Equal(0, new DateTime(2021,05,31).CompareTo(result));
        }
    }
}
