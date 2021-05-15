using StiebelEltronDashboard.Models;
using System.Threading.Tasks;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public interface IScrapingService
    {
        public Task<HeatPumpDatum> GetHeatPumpInformationAsync();
        public Task<HeatPumpDatum> GetHeatPumpInformationAsync(string sessionId = "");
    }
}