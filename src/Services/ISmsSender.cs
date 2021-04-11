using System.Threading.Tasks;

namespace StiebelEltronApiServer.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}