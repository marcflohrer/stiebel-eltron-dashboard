using System.Collections.Generic;
using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServer.Repositories
{
    public interface IHeatPumpDataRepository {
        public void InsertHeatPumpData (HeatPumpDatum heatPumpDatum);
        HeatPumpDatum[] GetLastYear ();
        public void RemoveRange (IEnumerable<HeatPumpDatum> heatPumpDatum);
    }
}