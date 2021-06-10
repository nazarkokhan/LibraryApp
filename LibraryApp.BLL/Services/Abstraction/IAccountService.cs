using System.Threading.Tasks;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;

namespace LibraryApp.BLL.Services.Abstraction
{
    public interface IAccountService
    {
        Task<Result> RegisterAsync(RegisterDto register);

        Task<Result<Token>> GetAccessTokenAsync(LogInUserDto userInput);

        Result<UserFromTokenDto> GetProfile();

        Task<Result> SendEmailResetTokenAsync(ResetEmailDto resetEmailDto);

        Task<Result> ResetEmailAsync(TokenEmailDto tokenEmailDto);

        Task<Result> SendPasswordResetTokenAsync(ResetPasswordDto resetPasswordDto);

        Task<Result> ResetPasswordAsync(TokenPasswordDto tokenPasswordDto);
    }
}