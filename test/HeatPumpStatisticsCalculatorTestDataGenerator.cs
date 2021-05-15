using System;
using System.Collections.Generic;
using System.Linq;
using StiebelEltronDashboard.Models;

namespace StiebelEltronDashboardTests {
    public class HeatPumpStatisticsCalculatorTestDataGenerator {
        public static IEnumerable<object[]> GetHeatPumpTestData () {
            return new List<object[]> {
                new object[] {
                    HeatPumpDatumFactory.Create (new DateTime(2021, 4, 15), 16, (i, time) => time.AddDays (i * 4)),
                        new StatisticsResult (new List<HeatPumpDatum> (),
                            HeatPumpDataPerPeriodFactory.Create (new DateTime (2021, 4, 15), 16, (i, time) => time.AddDays (i)).ToList ()
                        ),
                }
            };
        }
    }
}