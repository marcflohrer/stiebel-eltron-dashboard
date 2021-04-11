using System.Threading.Tasks;

namespace StiebelEltronApiServer.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}