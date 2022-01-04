using StiebelEltronDashboard.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace StiebelEltronDashboardTests
{
    public static class DateTimeExtensionsTestDataGenerator
    {
        public static IEnumerable<object[]> GetDateTimes()
        {
            yield return new object[] { new DateTime(2021, 12, 31), 52 };
            yield return new object[] { new DateTime(2022, 1, 1), 52 };
            yield return new object[] { new DateTime(2022, 1, 2), 52 };
            yield return new object[] { new DateTime(2022, 1, 3), 1 };
            yield return new object[] { new DateTime(2022, 1, 4), 1 };
        }
    }

    public class DateTimeExtensionsTests
    {
        [Theory]
        [MemberData(nameof(DateTimeExtensionsTestDataGenerator.GetDateTimes), MemberType = typeof(DateTimeExtensionsTestDataGenerator))]
        public void CorrectWeekNumberIsCalculated(DateTime dateTime, int expected)
        {
            // Act
            var actual = DateTimeExtensions.WeekOfYear(dateTime, new CultureInfo("de-DE"));

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
