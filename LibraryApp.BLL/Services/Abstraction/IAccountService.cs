using System.Threading.Tasks;
using LibraryApp.Core.DTO;

namespace LibraryApp.BLL.Services.Abstraction
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterDto register);

        Task<string> LogInAsync(LogInUserDto userInput);
        
        Task ResetEmailAsync(ChangeEmailDto emailDto);

        Task ResetPasswordAsync(ResetPasswordDto userDto);
    }
}