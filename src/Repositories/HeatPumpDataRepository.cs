using System;
using System.Collections.Generic;
using System.Linq;
using StiebelEltronDashboard.Models;

namespace StiebelEltronDashboard.Repositories
{
    public class HeatPumpDataRepository : IHeatPumpDataRepository {
        private readonly ApplicationDbContext _applicationDbContext;

        public HeatPumpDataRepository (ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public HeatPumpDatum[] GetAllRecordsOlderThan366Days(DateTime now)
        {
            var oneYearAgo = now.Subtract (TimeSpan.FromDays (366));
            return _applicationDbContext.HeatPumpData.Where (a => a.DateCreated < oneYearAgo).ToArray ();
        }

        public HeatPumpDatum[] GetLastYear () {
            var oneYearAgo = DateTime.UtcNow.Subtract (TimeSpan.FromDays (366));
            return _applicationDbContext.HeatPumpData.Where (a => a.DateCreated >= oneYearAgo).ToArray ();
        }
        public void InsertHeatPumpData (HeatPumpDatum heatPump) => _applicationDbContext.HeatPumpData.Add (heatPump);

        public void RemoveRange (IEnumerable<HeatPumpDatum> heatPumpData) => _applicationDbContext.HeatPumpData.RemoveRange (heatPumpData);

    }
}