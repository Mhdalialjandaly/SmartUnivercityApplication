
namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> RegisterAsync(string email, string password, string firstName, string lastName);
        Task<bool> LoginAsync(string email, string password);
        Task LogoutAsync();
    }
}
