
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace FileStorage.BLL
{
    /// <summary>
    /// Service for sending letters by email
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IOptions<SmtpOptions> _options;
        /// <summary>
        /// Creates instance of service
        /// </summary>
        /// <param name="options">Configuration for service</param>
        public EmailService(IOptions<SmtpOptions> options)
        {
            _options = options;
        }

        /// <summary>
        /// Send message by email
        /// </summary>
        /// <param name="message">Message wit data</param>
        /// <returns></returns>
        public async Task SendAsync(MailMessage message)
        {
            await Task.Run(() => {
                var email = new MimeMessage();

                email.From.Add(MailboxAddress.Parse(_options.Value.UserName));

                email.To.Add(MailboxAddress.Parse(message.To));

                email.Subject = message.Subject;

                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Body };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(_options.Value.Host, 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_options.Value.UserName, _options.Value.Password);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
            });
           


        }
    }
}
