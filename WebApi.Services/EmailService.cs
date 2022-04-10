using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using WebApi.Domain.CustomGenerator;
using WebApi.Domain.Interfaces.Services;

namespace WebApi.Services
{
    public class EmailService : IEmailService
    {
        private EmailServiceOptions Options { get; }
        
        public EmailService(IOptions<EmailServiceOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
        
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SEND_GRID_TOKEN, subject, message, email);
        }
        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.SEND_GRID_EMAIL, Options.SEND_GRID_USER),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}