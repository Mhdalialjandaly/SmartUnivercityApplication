using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IStudentAttendanceServices
    {
        Task<List<StudentAttendanceDto>> GetAllAttendancesAsync();
        Task<StudentAttendanceDto> GetAttendanceByIdAsync(int id);
        Task<StudentAttendanceDto> CreateAttendanceAsync(StudentAttendanceDto attendanceDto);
        Task UpdateAttendanceAsync(int id, StudentAttendanceDto attendanceDto);
        Task DeleteAttendanceAsync(int id);
        Task<bool> AttendanceExistsAsync(int id);

        // طرق خاصة بالحضور
        Task<List<StudentAttendanceDto>> GetAttendancesByStudentAsync(int studentId);
        Task<List<StudentAttendanceDto>> GetAttendancesByCourseAsync(int courseId);
        Task<List<StudentAttendanceDto>> GetAttendancesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<StudentsAttendanceSummaryDto> GetAttendanceSummaryAsync(int studentId, int courseId);
        Task<List<StudentsAttendanceReportDto>> GenerateAttendanceReportAsync(int courseId, DateTime startDate, DateTime endDate);

        Task<StudentsAttendanceSummaryDto> GetAttendanceSummaryAsync(); // ملخص عام
        Task<PaginatedResult<StudentAttendanceDto>> GetAttendancesPagedAsync(
            int page,
            int pageSize,
            string searchTerm,
            int? courseId,
            int? departmentId,
            DateTime? date,
            AttendanceStatus status);
    }
}
