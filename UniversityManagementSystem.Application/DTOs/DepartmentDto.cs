
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
        public UniversityDto University { get; set; }
        public IList<StudentDto> Students { get; set; }
        public IList<DepartmentDto> Departments { get; set; }
    }
}
