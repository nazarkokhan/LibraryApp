using System.Threading.Tasks;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;

namespace LibraryApp.BLL.Services.Abstraction
{
    public interface IAccountService
    {
        Task<Result> SendRegisterTokenAsync(RegisterDto register);

        Task<Result> ConfirmRegistrationAsync(string token, string userId);
        
        Task<Result<Token>> GetAccessTokenAsync(LogInUserDto userInput, int userId);

        Task<Result<ProfileDto>> GetProfile(int userId);

        Task<Result> SendEmailResetTokenAsync(ResetEmailDto resetEmailDto);

        Task<Result> ResetEmailAsync(TokenEmailDto tokenEmailDto);

        Task<Result> SendPasswordResetTokenAsync(ResetPasswordDto resetPasswordDto, int userId);

        Task<Result> ResetPasswordAsync(TokenPasswordDto tokenPasswordDto, int userId);
    }
}