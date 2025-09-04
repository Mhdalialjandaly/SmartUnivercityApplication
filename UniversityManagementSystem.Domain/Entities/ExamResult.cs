
namespace UniversityManagementSystem.Domain.Entities
{
    public class ExamResult : IEntity
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public decimal Score { get; set; }
        public decimal Percentage { get; set; }
        public string Grade { get; set; }
        public bool IsPassed { get; set; }
        public DateTime SubmissionDate { get; set; }

        // العلاقات
        public Exam Exam { get; set; }
        public Student Student { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        DateTime IEntity.DeletedAt { get ; set ; }
    }
}
