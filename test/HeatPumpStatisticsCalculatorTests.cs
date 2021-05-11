using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoMoqCore;
using StiebelEltronApiServer.Models;
using StiebelEltronApiServer.Services;
using StiebelEltronApiServerTests;
using Xunit;

namespace test {
    public class HeatPumpStatisticsCalculatorTests {
        
        [Theory]
        [MemberData(nameof(HeatPumpStatisticsCalculatorTestDataGenerator.GetHeatPumpTestData), MemberType = typeof(HeatPumpStatisticsCalculatorTestDataGenerator))]
        public void WhenCalculatingStatisticsCorrectStatisticsAreReturned (IEnumerable<HeatPumpDatum> heatPumpData, StatisticsResult expected) 
        {   
            // Arrange
            var autoMoqer = new AutoMoqer ();
            var fixture = new Fixture ();
            var start = new DateTime (2020, 4, 16);
            var now = new DateTime (2021, 5, 1);
            var statisticsService = autoMoqer.Create<StatisticsService>();
            autoMoqer.SetInstance<IStatisticsService>(statisticsService);
            var heatPumpStatisticsCalculator = autoMoqer.Create<HeatPumpStatisticsCalculator> ();

            // Act
            var actual = heatPumpStatisticsCalculator.Calculate (heatPumpData.ToList (), new List<HeatPumpDataPerPeriod>(), now);

            // Assert
            Assert.Equal (expected.DataSetsToRemove, actual.DataSetsToRemove);
            Assert.Equal(expected.Statistics.Count, actual.Statistics.Count);
            for(var i = 0; i < expected.Statistics.Count; ++i){
                Assert.Equal(expected.Statistics[i], actual.Statistics[i]);
            }
        }
    }
}