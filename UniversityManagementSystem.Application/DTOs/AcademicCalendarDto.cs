using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class AcademicCalendarDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CalendarEventType EventType { get; set; }
        public int? Semester { get; set; }
        public int AcademicYear { get; set; }
        public bool IsHoliday { get; set; }
        public bool IsUniversityWide { get; set; }
        public int? DepartmentId { get; set; }
        public DepartmentDto Department { get; set; }
    }
}
