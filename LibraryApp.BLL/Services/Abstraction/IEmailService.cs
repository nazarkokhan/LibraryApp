using System.Threading.Tasks;

namespace LibraryApp.BLL.Services.Abstraction
{
    public interface IEmailService
    {
        Task SendAsync(string to, string body, string subject = "");
    }
}