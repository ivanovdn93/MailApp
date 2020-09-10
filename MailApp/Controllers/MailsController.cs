using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MailApp.Models;
using MailApp.Services;
using Microsoft.Extensions.Options;

namespace MailApp.Controllers
{
    [Route("api/mails/")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        private readonly MailAppContext _context;

        private readonly IOptions<SmtpOptions> _smtpOptions;

        public MailsController(MailAppContext context, IOptions<SmtpOptions> smtpOptions)
        {
            _context = context;
            _smtpOptions = smtpOptions;
        }

        /// <summary>
        /// controller method that gets all sent mails from the database
        /// </summary>
        /// <returns>list of mails</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mail>>> GetMails()
        {
            foreach (var mail in _context.Mails.Include(m => m.RecipientList))
            {
                mail.Recipients = mail.RecipientList
                    .Select(r => r.Name)
                    .ToList();
            }

            return await _context.Mails.ToListAsync();
        }

        /// <summary>
        /// controller method sending an email and logging it in the database
        /// </summary>
        /// <param name="mail">json object with email fields</param>
        /// <returns>text result of sending an email</returns>
        [HttpPost]
        public async Task<string> PostMail(Mail mail)
        {
            mail.CreationDate = DateTime.Now;

            mail.RecipientList = mail.Recipients
                .Select(r => new Recipient { Name = r, MailId = mail.Id })
                .ToList();

            var emailService = new EmailService(_smtpOptions);
            try
            {
                emailService.SendEmail(mail.Recipients, mail.Subject, mail.Body);
                mail.Result = true;
            }
            catch(Exception e)
            {
                mail.Result = false;
                mail.FailedMessage = e.Message;
            }

            _context.Mails.Add(mail);
            await _context.SaveChangesAsync();

            return mail.Result ? "Сообщение успешно отправлено!"
                : "Сообщение не было отправлено! " + mail.FailedMessage;
        }
    }
}
