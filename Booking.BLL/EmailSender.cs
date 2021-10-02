using System.Threading.Tasks;
using Booking.Core.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Booking.BLL
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        void SendMail(string to, string subject, string message);
        void SendMailFromTemplate(string to, string subject, string template, string[] contents);
    }

    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<AppConfig> _options;
        private readonly IEmailHelper _emailHelper;

        public EmailSender(IOptions<AppConfig> options,
            IEmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
            _options = options;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }

        
        public void SendMail(string to, string subject, string message)
        {
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("BookingReservation", "hi@mojizze.com"));
            mail.To.Add(new MailboxAddress(to, to));
            mail.Subject = subject;

            mail.Body = new TextPart("html")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.CheckCertificateRevocation = false;
                client.Connect(_options.Value.MailHost,  _options.Value.MailPort);
                //client.AuthenticationMechanisms.Remove("XOAUTH2"); // Must be removed for Gmail SMTP
                client.Authenticate(_options.Value.MailUser, _options.Value.MailPassword);
                client.Send(mail);
                client.Disconnect(true);
            }
        }


        public void SendMailFromTemplate(string to, string subject, string template, string[] contents)
        {
            var builder = _emailHelper.BodyBuilder($"{template}.html");
            string messageBody = string.Format(builder.HtmlBody, contents);

            SendMail(to, subject, messageBody);
        }

    }
}
