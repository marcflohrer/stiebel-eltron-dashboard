using StiebelEltronApiServer.Models;
using System;
using System.Collections.Generic;

namespace StiebelEltronApiServer.Services
{
    public interface IStatisticsService
    {
        HeatPumpDataPerPeriod GetHeatPumpDataPerPeriod (IEnumerable<HeatPumpDatum> heatPumpData, int year, string periodKind, int periodNumber, DateTime now);
    }
}