using AuthenticationAPI.DTO;

namespace AuthenticationAPI.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterRequestDto dto);

        Task<AuthResponseDTO> LoginAsync(LoginRequestDto dto);

        Task LogoutAsync(string userId);
    }
}
