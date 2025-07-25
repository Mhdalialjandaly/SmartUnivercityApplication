
namespace UniversityManagementSystem.Application.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public int Credits { get; set; }
        public decimal Fee { get; set; }
        public int DepartmentId { get; set; }
        public string Instructor { get; set; } = "";
        public int MaxStudents { get; set; } = 30;
        public int CurrentStudents { get; set; } = 0;
    }
}
