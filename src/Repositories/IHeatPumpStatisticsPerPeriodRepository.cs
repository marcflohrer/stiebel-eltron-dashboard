using StiebelEltronApiServer.Models;
using System.Collections.Generic;

namespace StiebelEltronApiServer.Repositories
{
    public interface IHeatPumpStatisticsPerPeriodRepository
    {
        HeatPumpDataPerPeriod FindByYearAndPeriodNumber(int year, string periodNumber);
        void Add(HeatPumpDataPerPeriod heatPumpDataPerPeriod);
    }
}