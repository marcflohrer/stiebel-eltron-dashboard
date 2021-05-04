using StiebelEltronApiServer.Models;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Services.HtmlServices
{
    public interface IServiceWeltFacade
    {
        Task<ServiceWelt> GetHeatPumpWebsiteAsync(string sessionId);
    }
}