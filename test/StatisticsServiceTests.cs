using AutoFixture;
using AutoMoqCore;
using Moq;
using Newtonsoft.Json;
using stiebel_eltron_dashboard_tests.Extensions;
using StiebelEltronDashboard.Models;
using StiebelEltronDashboard.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace StiebelEltronDashboardTests
{
    public class StatisticsServiceTests
    {
        private ITestOutputHelper _testOutputHelper;
        public StatisticsServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [MemberData(nameof(StatisticsServiceTestDailyPeriodDataGenerator.GetHeatPumpTestData), MemberType = typeof(StatisticsServiceTestDailyPeriodDataGenerator))]
        public void WhenCalculatingStatisticsExpectedIsReturned(IEnumerable<HeatPumpDatum> heatPumpData, HeatPumpDataPerPeriod expectedHeatPumpDataPerPeriod)
        {
            var autoMoqer = new AutoMoqer();
            var fixture = new Fixture();
            var sessionId = fixture.Create<string>();
            var now = heatPumpData.Select(h => h.DateCreated).Min();

            var statisticsService = autoMoqer.Create<StatisticsService>();
            var year = heatPumpData.FirstOrDefault().DateCreated.Year;

            // Act
            var actualHeatPumpDataPerPeriod = statisticsService.GetHeatPumpDataPerPeriod(heatPumpData, year, "Day", 13, now);

            // Assert
            var expected = JsonConvert.SerializeObject(expectedHeatPumpDataPerPeriod);
            var actual = JsonConvert.SerializeObject(actualHeatPumpDataPerPeriod);
            if (expected != actual)
            {
                _testOutputHelper.WriteLine($"Expected: {expected}");
                _testOutputHelper.WriteLine($"Actual: {actual}");
            }
            try
            {
                Assert.Equal(expectedHeatPumpDataPerPeriod, actualHeatPumpDataPerPeriod);
            }
            catch
            {
                var changedProperties = expectedHeatPumpDataPerPeriod.ChangedProperties(actualHeatPumpDataPerPeriod);
                if (changedProperties.Count == 0)
                {
                    _testOutputHelper.WriteLine("All properties are as expected - yet the equals operator returns false.");
                }
                else
                {
                    _testOutputHelper.WriteLine("Changed properties are: " + string.Join(';', changedProperties));
                    foreach (var changedProperty in changedProperties)
                    {
                        var expectedProperty = expectedHeatPumpDataPerPeriod.GetValueOfProperty(changedProperty);
                        var actualProperty = actualHeatPumpDataPerPeriod.GetValueOfProperty(changedProperty);
                        try
                        {
                            Assert.Equal(expectedProperty, actualProperty);
                        }
                        catch
                        {
                            _testOutputHelper.WriteLine("Property " + changedProperty + " differs. Expected: " + expectedProperty + ", actual: " + actualProperty);
                        }
                    }
                }
            }
            Assert.Equal(expectedHeatPumpDataPerPeriod, actualHeatPumpDataPerPeriod);
        }
    }
}
