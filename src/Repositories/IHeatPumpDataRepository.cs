using System.Collections.Generic;
using StiebelEltronDashboard.Models;

namespace StiebelEltronDashboard.Repositories
{
    public interface IHeatPumpDataRepository {
        public void InsertHeatPumpData (HeatPumpDatum heatPumpDatum);
        HeatPumpDatum[] GetLastYear ();
        public void RemoveRange (IEnumerable<HeatPumpDatum> heatPumpDatum);
    }
}