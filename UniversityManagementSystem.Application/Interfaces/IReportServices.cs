using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IReportServices
    {
        Task<ReportSummaryDto> GetReportSummaryAsync();
        Task<List<CoursePerformanceDto>> GetCoursePerformanceAsync();
        Task<List<CourseDetailDto>> GetCourseDetailsAsync();
        Task<List<AttendanceReportDto>> GetAttendanceReportsAsync();
        Task<List<StudentPerformanceDto>> GetTopPerformingStudentsAsync(int count = 10);
        Task<List<DepartmentPerformanceDto>> GetDepartmentPerformanceAsync();
        Task<List<ExamPerformanceDto>> GetExamPerformanceAsync();
        Task<List<SemesterReportDto>> GetSemesterReportsAsync(int academicYear, int semester);
        Task<ReportSummaryDto> GetReportSummaryBySemesterAsync(int academicYear, int semester);
        Task<List<StudentPerformanceDto>> GetStudentsByGPARangeAsync(double minGPA, double maxGPA);
        Task<List<ExamPerformanceDto>> GetTopPerformingExamsAsync(int count = 5);
        Task<List<CourseDetailDto>> GetCoursesByDepartmentAsync(int departmentId);
    }
}
