
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int UniversityId { get; set; }
        public string Dean { get; set; }
        public int StudentCount { get; set; }
        public int FacultyCount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int CourseCount { get; set; } = 0;
        public int ProgramCount { get; set; } = 0;
        public int ResearchProjectCount { get; set; } = 0;
        public decimal SuccessRate { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public UniversityDto University { get; set; }
        public IList<StudentDto> Students { get; set; }
        public IList<DepartmentDto> Departments { get; set; }
    }
}
