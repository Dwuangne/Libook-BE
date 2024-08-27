using Libook_API.Models.DTO;

namespace Libook_API.Service.EmailService
{
    public interface IEmailService
    {
        Task SendConfirmEmailAsync(string toEmail, string subject, string templatePath, ConfirmEmailDTO confirmEmailDTO);
    }
}
