namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentAssessmentDto
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public int StudentId { get; set; }
        public decimal Score { get; set; }
        public string Feedback { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string AssessorName { get; set; }
    }
}