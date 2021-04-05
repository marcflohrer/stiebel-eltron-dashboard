using StiebelEltronApiServer.Models;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Services
{
    public interface IScrapingService
    {
        public Task<HeatPump> GetHeatPumpInformationAsync(bool retry = false);
    }
}