
namespace UniversityManagementSystem.Application.DTOs
{
    public class TopCourseDto
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string DepartmentName { get; set; }
        public int EnrolledStudentsCount { get; set; }
        public int Capacity { get; set; }
        public double EnrollmentPercentage { get; set; }
    }
}
