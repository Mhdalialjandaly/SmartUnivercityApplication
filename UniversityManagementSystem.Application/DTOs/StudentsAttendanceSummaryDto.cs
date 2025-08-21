namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentsAttendanceSummaryDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int TotalSessions { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int ExcusedCount { get; set; }
        public int LateCount { get; set; }
        public double AttendancePercentage { get; set; }
    }
}
