using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServer.Repositories
{
    public interface IHeatPumpDataRepository
    {
        public void InsertHeatPumpData(HeatPumpData heatPump);
    }
}