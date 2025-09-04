
namespace UniversityManagementSystem.Application.DTOs
{
    public class ReportSummaryDto
    {
        public int TotalStudents { get; set; }
        public int ActiveStudents { get; set; }
        public double AttendanceRate { get; set; }
        public double AverageRating { get; set; }
        public double SuccessRate { get; set; }
        public int TotalCourses { get; set; }
        public int TotalDepartments { get; set; }
        public double AverageGPA { get; set; }
        public int TotalExams { get; set; }
        public double AverageExamScore { get; set; }
    }
}
