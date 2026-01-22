using System.Threading.Tasks;

namespace Ideio.Core.Tools
{
    public interface IMailSender
    {
        Task SendMailAsync(string toEmail, string subject, string htmlBody, string name);
    }
}
