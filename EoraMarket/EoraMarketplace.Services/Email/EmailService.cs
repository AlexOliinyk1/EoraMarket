using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace EoraMarketplace.Services.Email
{
    public class EmailService : IIdentityMessageService
    {
        private readonly SmtpClient _client;

        public EmailService()
        {
            _client = new SmtpClient {
                EnableSsl = true
            };
        }

        public Task SendAsync(IdentityMessage message)
        {
            return Task.Factory.StartNew(() => SendEmail(message.Destination, message.Subject, message.Body));
        }

        public void SendEmail(string toAddress, string subject, string body, bool isBodyHtml = true)
        {
            var mailMessage = new MailMessage();

            mailMessage.To.Add(toAddress);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = isBodyHtml;

            try
            {
                _client.Send(mailMessage);
            }
            catch(Exception exc)
            {
            }
        }

    }
}
