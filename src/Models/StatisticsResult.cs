using System.Collections.Generic;

namespace StiebelEltronDashboard.Models
{
    public record StatisticsResult(IList<HeatPumpDatum> DataSetsToRemove, IList<HeatPumpDataPerPeriod> Statistics);
}