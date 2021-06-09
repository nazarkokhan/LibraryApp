using System.Threading.Tasks;
using LibraryApp.Core.DTO.Authorization;

namespace LibraryApp.BLL.Services.Abstraction
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterDto register);

        Task<string> GetAccessTokenAsync(LogInUserDto userInput);

        UserFromTokenDto GetProfile();

        Task SendEmailResetTokenAsync(ResetEmailDto resetEmailDto);

        Task ResetEmailAsync(TokenEmailDto tokenEmailDto);

        Task SendPasswordResetTokenAsync(ResetPasswordDto resetPasswordDto);

        Task ResetPasswordAsync(TokenPasswordDto tokenPasswordDto);
    }
}