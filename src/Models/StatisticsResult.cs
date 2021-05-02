using System.Collections.Generic;

namespace StiebelEltronApiServer.Models
{
    public record StatisticsResult(IList<HeatPumpDatum> DataSetsToRemove, IList<HeatPumpDataPerPeriod> Statistics);
}