using Azure.Security.KeyVault.Secrets;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using webapi.Models.Request;
using webapi.Models.Settings;

namespace webapi.Services.Email
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailSettings _emailSettings;
        private readonly IConfiguration _configuration;
        private readonly SecretClient _secretClient;

        public EmailServices(IOptions<EmailSettings> options, IConfiguration configuration, SecretClient secretClient)
        {
            _emailSettings = options.Value;
            _configuration = configuration;
            _secretClient = secretClient;
        }

        public async Task SendEmailAsync(EmailRequest request)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Email));
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = request.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = request.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            /*var password = _configuration["EmailSettings:Password"];*/
            var password = _secretClient.GetSecret("EmailSettings-Password").Value.Value.ToString();
            smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Email, password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}