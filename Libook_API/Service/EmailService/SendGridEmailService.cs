using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.AspNetCore.Routing.Template;
using Libook_API.Models.DTO;

namespace Libook_API.Service.EmailService
{
    public class SendGridEmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public SendGridEmailService(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }

        public async Task SendConfirmEmailAsync(string toEmail, string subject, string templatePath, ConfirmEmailDTO confirmEmailDTO)
        {

            var templateFilePath = Path.Combine(env.ContentRootPath, templatePath);

            var body = await File.ReadAllTextAsync(templateFilePath);

            // Replace placeholders in the template with actual data
            body = body.Replace("@Model.Username", confirmEmailDTO.Username)
                       .Replace("@Model.ConfirmationLink", confirmEmailDTO.ConfirmationLink);

            var client = new SendGridClient(configuration["SendGridSettings:ApiKey"]);
            var from = new EmailAddress(configuration["SendGridSettings:SenderEmail"], configuration["SendGridSettings:SenderName"]);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, body);
            msg.HtmlContent = body;

            // Đường dẫn tới file ảnh
            var rootPath = env.WebRootPath ?? env.ContentRootPath;
            var imagePath = Path.Combine(rootPath, "Images", "Logo_Libook", "Logo_Libook_RemovedBg.png");

            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException("Image file not found.", imagePath);
            }

            if (File.Exists(imagePath))
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                var imageBase64 = Convert.ToBase64String(imageBytes);
                msg.AddAttachment("Logo_Libook.png", imageBase64, "image/png", "inline", "Logo_Libook");
            }

            var response = await client.SendEmailAsync(msg);
        }
    }
}
