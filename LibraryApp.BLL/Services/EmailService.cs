using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LibraryApp.BLL.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LibraryApp.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;

        public EmailService(IConfiguration configuration, IHttpContextAccessor accessor)
        {
            _configuration = configuration;
            _accessor = accessor;
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