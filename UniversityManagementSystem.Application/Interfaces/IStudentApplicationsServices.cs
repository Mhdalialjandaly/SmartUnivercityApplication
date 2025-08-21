using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IStudentApplicationsServices
    {
        Task<List<StudentApplicationDto>> GetAllApplicationsAsync();
        Task<StudentApplicationDto> GetApplicationByIdAsync(int id);
        Task<StudentApplicationDto> CreateApplicationAsync(StudentApplicationDto dto);
        Task UpdateApplicationAsync(int id, StudentApplicationDto dto);
        Task DeleteApplicationAsync(int id);
        Task<bool> ApproveApplicationAsync(int id);
        Task<bool> RejectApplicationAsync(int id, string reason);
        Task<List<StudentApplicationDto>> GetPendingApplicationsAsync();
        Task<int> GetApplicationsCountAsync();
        Task<int> GetPendingCountAsync();
        Task<int> GetApprovedCountAsync();
        Task<int> GetRejectedCountAsync();
    }
}
