using System.Threading.Tasks;

namespace StiebelEltronApiserver.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}