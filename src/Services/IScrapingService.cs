using StiebelEltronApiServer.Models;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Services
{
    public interface IScrapingService
    {
        public Task<HeatPumpDatum> GetHeatPumpInformationAsync();
        public Task<HeatPumpDatum> GetHeatPumpInformationAsync(string sessionId = "");
    }
}