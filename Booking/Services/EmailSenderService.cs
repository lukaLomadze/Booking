using System.Net.Mail;
using System.Net;
using Booking.Interfaces;

namespace Booking.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _config;
        public EmailSenderService(IConfiguration config)
        {
            _config = config;

        }

        public async Task SendEmail(string mail, string subject, string body)
        {
            var email = _config.GetValue<string>("EmailConfiguration:EMAIL");
            var pass = _config.GetValue<string>("EmailConfiguration:PASSWORD");
            var host = _config.GetValue<string>("EmailConfiguration:HOST");
            var port = _config.GetValue<int>("EmailConfiguration:PORT");

            var client = new SmtpClient(host, port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email, pass)
            };
            var mailMessage = new MailMessage(
                from: email!,
                to: mail,
                subject,
                body);
            mailMessage.IsBodyHtml = true;

            await client.SendMailAsync(mailMessage);




        }



    }
}

