using StiebelEltronApiServer.Models;
using System;
using System.Collections.Generic;

namespace StiebelEltronApiServer.Services
{
    public interface IHeatPumpStatisticsCalculator
    {
        StatisticsResult Calculate(IList<HeatPumpDatum> heatPumpData, IList<HeatPumpDataPerPeriod> heatPumpDataPerPeriods, DateTime now);
    }
}