using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class AcademicCalendar : IEntity
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

        // العلاقات
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
