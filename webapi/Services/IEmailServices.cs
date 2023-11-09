using webapi.Models;

namespace webapi.Services
{
    public interface IEmailServices
    {
        Task SendEmailAsync(EmailRequest request);
    }
}