using StiebelEltronApiServer.Models;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Repositories
{
    public interface IHeatPumpDataRepository
    {
        public void InsertHeatPumpData(HeatPumpDatum heatPumpDatum);

        public Task<HeatPumpDatum> GetMaxTotalPowerConsumption();
    }
}