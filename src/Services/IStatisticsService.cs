using StiebelEltronDashboard.Models;
using System;
using System.Collections.Generic;

namespace StiebelEltronDashboard.Services
{
    public interface IStatisticsService
    {
        HeatPumpDataPerPeriod GetHeatPumpDataPerPeriod (IEnumerable<HeatPumpDatum> heatPumpData, int year, string periodKind, int periodNumber, DateTime now);
    }
}