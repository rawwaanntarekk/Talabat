using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;

namespace LinkDev.Talabat.Core.Application.Abstraction.Auth
{
    public interface IAuthService
    {
        Task<UserDTO> LoginAsync(LoginDTO model);
        Task<UserDTO> RegisterAsync(RegisterDTO model);
    }
}
