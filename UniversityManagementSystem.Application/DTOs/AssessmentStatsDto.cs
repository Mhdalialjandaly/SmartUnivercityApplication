namespace UniversityManagementSystem.Application.DTOs
{
    public class AssessmentStatsDto
    {
        public int TotalAssessments { get; set; }
        public int CompletedAssessments { get; set; }
        public int PendingAssessments { get; set; }
        public int InProgressAssessments { get; set; }
        public int UpcomingAssessments { get; set; }
        public decimal AverageCompletionRate { get; set; }
        public int TotalStudentSubmissions { get; set; }
        public decimal AverageScore { get; set; }
    }
}