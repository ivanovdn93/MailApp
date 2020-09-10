using System.Collections.Generic;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace MailApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpOptions _smtpOptions;

        public EmailService(IOptions<SmtpOptions> smtpOptions)
        {
            _smtpOptions = smtpOptions.Value;
        }

        /// <summary>
        /// sends an email by params
        /// </summary>
        /// <param name="recipients">list of recipients mails</param>
        /// <param name="subject">message subject</param>
        /// <param name="message">message text</param>
        public void SendEmail(IEnumerable<string> recipients, string subject, string message)
        {
            var email = new MimeMessage {Sender = MailboxAddress.Parse(_smtpOptions.User) };
            foreach (var recipient in recipients)
            {
                email.To.Add(MailboxAddress.Parse(recipient));
            }
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = message };

            using var smtp = new SmtpClient();
            smtp.Connect(_smtpOptions.Host, _smtpOptions.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_smtpOptions.User, _smtpOptions.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}