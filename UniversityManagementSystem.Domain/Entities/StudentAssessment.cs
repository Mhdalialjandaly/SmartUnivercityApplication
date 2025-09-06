namespace UniversityManagementSystem.Domain.Entities
{
    public class StudentAssessment : IEntity
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public decimal Score { get; set; }
        public string Feedback { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string AssessorName { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set; }
    }
}