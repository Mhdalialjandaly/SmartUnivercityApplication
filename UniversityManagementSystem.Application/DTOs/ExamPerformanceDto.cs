namespace UniversityManagementSystem.Application.DTOs
{
    public class ExamPerformanceDto
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public double AverageScore { get; set; }
        public double PassRate { get; set; }
        public double HighestScore { get; set; }
        public double LowestScore { get; set; }
        public int TotalStudents { get; set; }
        public DateTime ExamDate { get; set; }
        public string ExamType { get; set; }
    }
}
