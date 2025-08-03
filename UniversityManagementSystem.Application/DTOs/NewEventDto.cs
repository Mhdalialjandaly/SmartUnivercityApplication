using UniversityManagementSystem.Domain.Enums;
namespace UniversityManagementSystem.Application.DTOs
{
    public class NewEventDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public CalendarEventType EventType { get; set; }
        public int? DepartmentId { get; set; }
    }
}
