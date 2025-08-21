using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface ICourseServices
    {
        Task<List<CourseDto>> GetAllCoursesAsync();
        Task<List<CourseDto>> GetCoursesByDepartmentAsync(int departmentId);
        Task<List<CourseDto>> GetActiveCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<CourseDto> CreateCourseAsync(CourseDto courseDto);
        Task UpdateCourseAsync(int id, CourseDto courseDto);
        Task DeleteCourseAsync(int id);
        Task<bool> CourseExistsAsync(int id);
        Task<CourseDto> GetCourseByCodeAsync(string code);
        Task<int> GetCoursesCountAsync();
        Task<int> GetActiveCoursesCountAsync();
        Task<List<CourseDto>> GetCoursesByInstructorAsync(string instructorName);
        Task<decimal> GetCourseFeeAsync(int courseId);
        Task UpdateCourseFeeAsync(int courseId, decimal newFee);
        Task<List<CourseDto>> SearchCoursesAsync(string searchTerm);
        Task<CourseSearchResult> GetCoursesAsync(int pageNumber, int pageSize, string searchTerm, int? departmentId, bool? isActive);
        Task<CourseStatisticsDto> GetCourseStatisticsAsync();
        Task<List<CourseDto>> GetPopularCoursesAsync(int count);
        Task<decimal> GetTotalRevenueAsync();
        Task<List<CourseDto>> GetCoursesBySemesterAsync(int semester, int academicYear);
        Task<bool> IsCourseFullAsync(int courseId);
        Task<int> GetAvailableSeatsAsync(int courseId);
        Task<List<CourseDto>> GetPrerequisiteCoursesAsync(string prerequisites);

        Task<decimal> GetTotalCourseRevenueAsync(int semester, int year);

        Task<List<CourseRevenueDto>> GetTopRevenueCoursesAsync(int count = 10);
        Task<List<Course>> GetTopEnrolledCoursesAsync(
       int topCount = 5,
       string academicYear = null,
       int? departmentId = null);
    }
}
