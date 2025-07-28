
namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentSearchResult
    {
        public List<StudentDto> Students { get; set; } = new();
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
