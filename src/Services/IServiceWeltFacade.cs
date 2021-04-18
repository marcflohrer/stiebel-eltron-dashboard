using StiebelEltronApiServer.Models;
using System.Threading.Tasks;

namespace StiebelEltronApiServer.Services
{
    public interface IServiceWeltFacade
    {
        Task<ServiceWelt> GetHeatPumpWebsiteAsync(string sessionId);
    }
}