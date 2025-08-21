
namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentsAttendanceReportDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int TotalSessions { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int ExcusedCount { get; set; }
        public double AttendancePercentage { get; set; }
        public string Status { get; set; }
    }
}
