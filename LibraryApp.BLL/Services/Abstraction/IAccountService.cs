using System.Threading.Tasks;
using LibraryApp.Core.DTO;

namespace LibraryApp.BLL.Services.Abstraction
{
    public interface IAccountService
    {
        Task Register(RegisterDto register);

        Task<UserFromTokenDto> GetProfile();

        Task ResetEmailAsync(ResetEmailDto emailDto);

        Task ResetPasswordAsync(ResetPasswordDto userDto);
    }
}