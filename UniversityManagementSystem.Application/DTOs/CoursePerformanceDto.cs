
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.DTOs
{
    public class CoursePerformanceDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public double AverageScore { get; set; }
        public int StudentCount { get; set; }
        public double PassRate { get; set; }
        public double AverageAttendance { get; set; }
        public string DepartmentName { get; set; }
        public EnrollmentDto Enrollments { get; set; }
    }
}
