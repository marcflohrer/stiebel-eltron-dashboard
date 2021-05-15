using StiebelEltronDashboard.Models;
using System;
using System.Collections.Generic;

namespace StiebelEltronDashboard.Services
{
    public interface IHeatPumpStatisticsCalculator
    {
        StatisticsResult Calculate(IList<HeatPumpDatum> heatPumpData, IList<HeatPumpDataPerPeriod> heatPumpDataPerPeriods, DateTime now);
    }
}