using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using Microsoft.Extensions.Configuration;

namespace LibraryApp.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpClient = new SmtpClient(_configuration["EmailSettings:Host"], int.Parse(_configuration["EmailSettings:Port"]))
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_configuration["EmailSettings:UserName"], _configuration["EmailSettings:Password"])
            };
        }

        public async Task SendAsync(string to, string body, string subject = "")
        {
            await _smtpClient.SendMailAsync(_configuration["EmailSettings:Email"], to, subject, body);
        }
    }
}