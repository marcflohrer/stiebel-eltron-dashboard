using StiebelEltronApiServer.Models;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Services
{
    public interface IScrapingService
    {
        public Task<HeatPumpData> GetHeatPumpInformationAsync();
        public Task<HeatPumpData> GetHeatPumpInformationAsync(string sessionId = "");
    }
}