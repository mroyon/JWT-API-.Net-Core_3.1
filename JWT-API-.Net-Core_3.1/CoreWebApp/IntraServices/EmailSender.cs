using BDO.DataAccessObjects.CommonEntities;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CoreWebApp.IntraServices
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<EmailSettings> _optionsEmailSettings;

        public EmailSender(IOptions<EmailSettings> optionsEmailSettings)
        {
            _optionsEmailSettings = optionsEmailSettings;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {

            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_optionsEmailSettings.Value.UsernameEmail, _optionsEmailSettings.Value.FromEmail)
                };
                mail.To.Add(new MailAddress(email));
                mail.CC.Add(new MailAddress(_optionsEmailSettings.Value.CcEmail));

                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_optionsEmailSettings.Value.SecondayDomain, _optionsEmailSettings.Value.SecondaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_optionsEmailSettings.Value.UsernameEmail, _optionsEmailSettings.Value.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                //do something here
            }
        }
    }
}
