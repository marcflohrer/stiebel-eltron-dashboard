using System.Threading.Tasks;

namespace StiebelEltronDashboard.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}