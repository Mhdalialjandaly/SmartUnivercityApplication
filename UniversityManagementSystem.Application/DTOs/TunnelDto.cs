
namespace UniversityManagementSystem.Application.DTOs
{
    public class TunnelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeOfkinship { get; set; }
        public string FirstPart { get; set; }
        public string SecoundPart { get; set; }
        public List<StudentDto> Students { get; set; }
    }
}
