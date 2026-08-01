using AuthenticationAPI.DTO;

namespace AuthenticationAPI.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(string id);
        Task<bool> DeleteAsync(string id);
        Task<bool> AssignRoleAsync(string id, string role);
    }
}
