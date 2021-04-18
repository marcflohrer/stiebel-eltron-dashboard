using StiebelEltronApiServer.Models;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Repositories
{
    public interface IHeatPumpDataRepository
    {
        public void InsertHeatPumpData(HeatPumpData heatPump);

        public Task<HeatPumpData> GetMaxTotalPowerConsumption();
    }
}