using System.Threading.Tasks;

namespace StiebelEltronDashboard.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}