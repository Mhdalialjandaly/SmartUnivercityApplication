namespace UniversityManagementSystem.Application.DTOs
{
    public class CourseStatisticsDto
    {
        public int TotalCourses { get; set; }
        public int ActiveCourses { get; set; }
        public int InactiveCourses { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalStudents { get; set; }
        public double AverageCredits { get; set; }
    }
}
