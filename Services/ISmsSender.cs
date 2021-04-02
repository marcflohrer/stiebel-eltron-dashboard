using System.Threading.Tasks;

namespace StiebelEltronApiserver.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}