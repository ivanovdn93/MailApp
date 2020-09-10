using System.Collections.Generic;

namespace MailApp.Services
{
    public interface IEmailService
    {
        void SendEmail(IEnumerable<string> recipients, string subject, string message);
    }
}
