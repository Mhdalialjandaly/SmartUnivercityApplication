namespace UniversityManagementSystem.Application.DTOs
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Room { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
    }
}
