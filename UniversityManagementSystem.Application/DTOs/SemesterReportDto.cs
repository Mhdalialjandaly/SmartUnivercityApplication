
namespace UniversityManagementSystem.Application.DTOs
{
    public class SemesterReportDto
    {
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }
        public double AverageGPA { get; set; }
        public double PassRate { get; set; }
        public double AttendanceRate { get; set; }
        public int TotalExams { get; set; }
        public double AverageEvaluation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
