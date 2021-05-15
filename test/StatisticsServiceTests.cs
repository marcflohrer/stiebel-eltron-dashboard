using AutoFixture;
using AutoMoqCore;
using Moq;
using Newtonsoft.Json;
using StiebelEltronApiServer.Models;
using StiebelEltronApiServer.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace StiebelEltronApiServerTests
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
            if(expected != actual){
                 _testOutputHelper.WriteLine($"Expected: {expected}");   
                 _testOutputHelper.WriteLine($"Actual: {actual}");
            }
            
            Assert.Equal(expectedHeatPumpDataPerPeriod, actualHeatPumpDataPerPeriod);
        }
    }
}
