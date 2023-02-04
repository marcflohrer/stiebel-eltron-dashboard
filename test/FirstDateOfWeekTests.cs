using StiebelEltronDashboard.Services;
using System;
using System.Globalization;
using Xunit;

namespace stiebel_eltron_dashboard_tests
{
    public class FirstDateOfWeekTests
    {
        [Theory]
        [InlineData(2022, 1, "de-DE", "2022-1-3")]
        [InlineData(2022, 2, "de-DE", "2022-1-10")]
        [InlineData(2020, 53, "de-DE", "2020-12-28")]
        [InlineData(2019, 1, "de-DE", "2018-12-31")]
        [InlineData(2019, 52, "de-DE", "2019-12-23")]
        public void TestFirstDateOfWeek(int year, int weekOfYear, string cultureName, string expectedDate)
        {
            var ci = new CultureInfo(cultureName);
            DateTime result = PeriodDateProvider.FirstDateOfWeek(year, weekOfYear, ci);
            Assert.Equal(DateTime.Parse(expectedDate), result);
        }
    }
}

