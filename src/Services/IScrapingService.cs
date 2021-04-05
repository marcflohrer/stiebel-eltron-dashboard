using StiebelEltronApiServer.Models;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Services
{
    public interface IScrapingService
    {
        public Task<HeatPump> GetHeatPumpInformationAsync();
        public Task<HeatPump> GetHeatPumpInformationAsync(string sessionId = "", bool retry = false);
    }
}