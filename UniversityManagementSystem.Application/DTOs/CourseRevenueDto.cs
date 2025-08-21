

namespace UniversityManagementSystem.Application.DTOs
{
    public class CourseRevenueDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int CurrentStudents { get; set; }
        public decimal Fee { get; set; }
        public decimal TotalRevenue { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public decimal Percentage { get; set; }
    }
}
