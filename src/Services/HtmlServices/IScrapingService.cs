using StiebelEltronApiServer.Models;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Services.HtmlServices
{
    public interface IScrapingService
    {
        public Task<HeatPumpDatum> GetHeatPumpInformationAsync();
        public Task<HeatPumpDatum> GetHeatPumpInformationAsync(string sessionId = "");
    }
}