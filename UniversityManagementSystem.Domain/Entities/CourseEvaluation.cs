
namespace UniversityManagementSystem.Domain.Entities
{
    public class CourseEvaluation : IEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Rating { get; set; } // من 1 إلى 5
        public string Comment { get; set; }
        public DateTime EvaluationDate { get; set; }

        // العلاقات
        public Student Student { get; set; }
        public Course Course { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        DateTime IEntity.DeletedAt { get ; set ; }
    }
}
