using System;
using System.Collections.Generic;
using StiebelEltronDashboard.Models;

namespace StiebelEltronDashboard.Repositories
{
    public interface IHeatPumpDataRepository {
        public void InsertHeatPumpData (HeatPumpDatum heatPumpDatum);
        HeatPumpDatum[] GetLastYear ();
        HeatPumpDatum[] GetAllRecordsOlderThan366Days(DateTime now);
        public void RemoveRange (IEnumerable<HeatPumpDatum> heatPumpDatum);
    }
}