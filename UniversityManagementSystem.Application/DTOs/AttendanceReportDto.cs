namespace UniversityManagementSystem.Application.DTOs
{
    public class AttendanceReportDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int TotalLectures { get; set; }
        public int PresentStudents { get; set; }
        public int AbsentStudents { get; set; }
        public double AttendanceRate { get; set; }
        public string DepartmentName { get; set; }
        public int Semester { get; set; }
        public int AcademicYear { get; set; }
    }
}
