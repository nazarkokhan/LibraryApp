using System.Threading.Tasks;
using LibraryApp.Core.DTO.Authorization;
using LibraryApp.Core.DTO.Authorization.Reset;
using LibraryApp.Core.ResultModel;
using LibraryApp.Core.ResultModel.Generics;

namespace LibraryApp.BLL.Services.Abstraction
{
    public interface IAccountService
    {
        Task<Result> CreateUserAndSendEmailTokenAsync(RegisterDto register);

        Task<Result> ConfirmRegistrationAsync(string token, string userId);
        
        Task<Result<Token>> GetAccessTokenAsync(LogInUserDto userInput);

        Task<Result<ProfileDto>> GetProfile(int userId);

        Task<Result> SendEmailResetTokenAsync(ResetEmailDto resetEmailDto, int userId);

        Task<Result> ResetEmailAsync(string token, string newEmail, int userId);

        Task<Result> SendPasswordResetTokenAsync(ResetPasswordDto resetPasswordDto);

        Task<Result> ResetPasswordAsync(TokenPasswordDto tokenPasswordDto);
    }
}