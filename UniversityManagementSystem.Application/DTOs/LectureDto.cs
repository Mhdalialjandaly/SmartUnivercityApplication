
namespace UniversityManagementSystem.Application.DTOs
{
    public class LectureDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; } 
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime LectureDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; } 
        public string LectureType { get; set; } 
        public bool IsCancelled { get; set; }
        public string Notes { get; set; } 
        public DateTime CreatedDate { get; set; }

        // الإحصائيات
        public int TotalStudents { get; set; }
        public int PresentStudents { get; set; }
        public int AbsentStudents { get; set; }
    }

    public class LectureSearchResult
    {
        public List<LectureDto> Lectures { get; set; } 
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
