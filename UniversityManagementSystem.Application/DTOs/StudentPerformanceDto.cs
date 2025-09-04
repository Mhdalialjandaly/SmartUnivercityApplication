
namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentPerformanceDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentIdNumber { get; set; }
        public double GPA { get; set; }
        public int TotalCredits { get; set; }
        public int CoursesCount { get; set; }
        public double AttendanceRate { get; set; }
        public string AcademicYear { get; set; }
        public int Semester { get; set; }
    }
}
