using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface ICourseRegistrationServices
    {
        Task<List<CourseRegistrationDto>> GetAllRegistrationsAsync();
        Task<List<CourseRegistrationDto>> GetRegistrationsByStudentIdAsync(int studentId);
        Task<List<CourseRegistrationDto>> GetRegistrationsByCourseIdAsync(int courseId);
        Task<List<CourseRegistrationDto>> GetRegistrationsByStatusAsync(string status);
        Task<CourseRegistrationDto?> GetRegistrationByIdAsync(int id);
        Task<CourseRegistrationDto> CreateRegistrationAsync(CourseRegistrationDto registrationDto);
        Task UpdateRegistrationAsync(int id, CourseRegistrationDto registrationDto);
        Task DeleteRegistrationAsync(int id);
        Task<bool> RegistrationExistsAsync(int id);
        Task<decimal> GetTotalAmountPaidByStudentAsync(int studentId);
        Task<int> GetStudentRegistrationCountAsync(int studentId);
        Task<int> GetCourseRegistrationCountAsync(int courseId);
        Task<List<CourseRegistrationDto>> GetRecentRegistrationsAsync(int count);
        Task<decimal> GetTotalRevenueAsync();
        Task<List<CourseRegistrationDto>> GetRegistrationsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task CancelRegistrationAsync(int id);
        Task CompleteRegistrationAsync(int id);
        Task<bool> IsStudentRegisteredAsync(int studentId, int courseId);

        Task<PaginatedResult<CourseRegistrationDto>> GetRegistrationsPagedAsync(int pageNumber, int pageSize, string searchTerm = null, int? departmentId = null,string status = null);
        Task<CourseRegistrationSummaryDto> GetRegistrationSummaryAsync();
    }
}
