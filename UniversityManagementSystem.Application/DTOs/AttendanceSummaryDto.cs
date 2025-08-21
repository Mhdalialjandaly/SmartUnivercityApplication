
namespace UniversityManagementSystem.Application.DTOs
{
    public class AttendanceSummaryDto
    {
        public int TotalEmployees { get; set; }
        public int TotalSessions { get; set; }
        public int ExcusedCount { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LeaveCount { get; set; }
        public decimal AverageHours { get; set; }
    }
}
