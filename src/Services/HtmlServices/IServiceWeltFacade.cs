using StiebelEltronDashboard.Models;
using System.Threading.Tasks;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public interface IServiceWeltFacade
    {
        Task<ServiceWelt> GetHeatPumpWebsiteAsync(string sessionId);
    }
}