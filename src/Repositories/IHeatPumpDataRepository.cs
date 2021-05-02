using StiebelEltronApiServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Repositories
{
    public interface IHeatPumpDataRepository
    {
        public void InsertHeatPumpData(HeatPumpDatum heatPumpDatum);

        public Task<HeatPumpDatum> GetMaxTotalPowerConsumption();
        HeatPumpDatum[] GetLastYear();
        
        public void RemoveRange(IEnumerable<HeatPumpDatum> heatPumpDatum);
    }
}