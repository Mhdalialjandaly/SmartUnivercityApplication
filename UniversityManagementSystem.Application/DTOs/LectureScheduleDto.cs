

namespace UniversityManagementSystem.Application.DTOs
{
    public class LectureScheduleDto
    {
        public DateTime Date { get; set; }
        public List<LectureDto> Lectures { get; set; }
    }
}
