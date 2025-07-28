using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<List<UserDto>> GetUsersByRoleAsync(string role);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task UpdateUserAsync(int id, UserDto userDto);
        Task DeleteUserAsync(int id);
        Task<bool> UserExistsAsync(string id);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<List<UserDto>> GetActiveUsersAsync();
        Task<int> GetUsersCountAsync();
        Task<List<string>> GetUserRolesAsync(int userId);
        Task AssignRoleAsync(int userId, string role);
        Task RemoveRoleAsync(int userId, string role);
    }
}
