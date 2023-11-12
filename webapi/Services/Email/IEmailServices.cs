using webapi.Models.Request;

namespace webapi.Services.Email
{
    public interface IEmailServices
    {
        Task SendEmailAsync(EmailRequest request);
    }
}