using System.Threading.Tasks;

namespace stiebel_eltron_apiserver.Core.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string from, string subject, string body);
    }
}
