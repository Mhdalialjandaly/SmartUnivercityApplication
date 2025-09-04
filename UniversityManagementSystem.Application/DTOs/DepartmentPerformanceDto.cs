
namespace UniversityManagementSystem.Application.DTOs
{
    public class DepartmentPerformanceDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int TotalStudents { get; set; }
        public double AverageGPA { get; set; }
        public int CoursesCount { get; set; }
        public double SuccessRate { get; set; }
        public double AverageAttendanceRate { get; set; }
        public int TotalFaculty { get; set; }
        public List<EnrollmentDto> Enrollments { get; set; }
    }
}
