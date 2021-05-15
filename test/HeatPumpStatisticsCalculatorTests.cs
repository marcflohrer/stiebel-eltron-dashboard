using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoMoqCore;
using StiebelEltronApiServer.Models;
using StiebelEltronApiServer.Services;
using StiebelEltronApiServerTests;
using Xunit;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace test
{
    public class HeatPumpStatisticsCalculatorTests
    {
        private ITestOutputHelper _testOutputHelper;
        public HeatPumpStatisticsCalculatorTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Theory]
        [MemberData(nameof(HeatPumpStatisticsCalculatorTestDataGenerator.GetHeatPumpTestData), MemberType = typeof(HeatPumpStatisticsCalculatorTestDataGenerator))]
        public void WhenCalculatingStatisticsCorrectStatisticsAreReturned(IEnumerable<HeatPumpDatum> heatPumpData, StatisticsResult expected)
        {
            // Arrange
            var autoMoqer = new AutoMoqer();
            var fixture = new Fixture();
            var start = new DateTime(2020, 4, 16);
            var now = new DateTime(2021, 5, 1);
            var statisticsService = autoMoqer.Create<StatisticsService>();
            autoMoqer.SetInstance<IStatisticsService>(statisticsService);
            var heatPumpStatisticsCalculator = autoMoqer.Create<HeatPumpStatisticsCalculator>();

            // Act
            var actual = heatPumpStatisticsCalculator.Calculate(heatPumpData.ToList(), new List<HeatPumpDataPerPeriod>(), now);

            // Assert
            Assert.Equal(expected.DataSetsToRemove, actual.DataSetsToRemove);
            Assert.Equal(expected.Statistics.Count, actual.Statistics.Count);
            for (var i = 0; i < expected.Statistics.Count; ++i)
            {
                var expectedString = JsonConvert.SerializeObject(expected.Statistics[i]);
                var actualString = JsonConvert.SerializeObject(actual.Statistics[i]);
                if(expectedString != actualString){
                    _testOutputHelper.WriteLine($"{i}::Expected: {expectedString} - Actual: {actualString}");
                }
                Assert.Equal(expectedString, actualString);
            }
        }
    }
}