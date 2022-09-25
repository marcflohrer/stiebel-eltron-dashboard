using StiebelEltronDashboard.Models;
using System.Threading.Tasks;
using static StiebelEltronDashboard.Services.HtmlServices.ServiceWeltService;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public interface IServiceWeltService
    {
        public Task<HeatPumpDatum> GetHeatPumpInformationAsync();
        public Task<HeatPumpDatum> GetHeatPumpInformationAsync(string sessionId = "");
        Task<LanguageName> GetCurrentLanguageSettingAsync(string sessionId = "1997d0dc84ee423f6b46fcd7ae1a3891");
        Task<bool> SetLanguageAsync(string language, string sessionId = "1997d0dc84ee423f6b46fcd7ae1a3891");
    }
}