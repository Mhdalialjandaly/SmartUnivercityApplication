
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Exam : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public DateTime ExamDate { get; set; }
        public int Duration { get; set; } // بالدقائق
        public decimal TotalMarks { get; set; }
        public ExamType ExamType { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        // العلاقات
        public Course Course { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        DateTime IEntity.DeletedAt { get ; set ; }
    }
}
