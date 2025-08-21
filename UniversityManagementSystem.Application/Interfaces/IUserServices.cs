using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IUserServices
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<List<UserDto>> GetUsersByRoleAsync(string role);
        Task<UserDto> GetUserByIdAsync(string id);
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task UpdateUserAsync(string id, UserDto userDto);
        Task DeleteUserAsync(string id);
        Task<bool> UserExistsAsync(string id);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<List<UserDto>> GetActiveUsersAsync();
        Task<int> GetUsersCountAsync();
        Task<List<string>> GetUserRolesAsync(string userId);
        Task AssignRoleAsync(string userId, string role);
        Task RemoveRoleAsync(string userId, string role);

    }
}
