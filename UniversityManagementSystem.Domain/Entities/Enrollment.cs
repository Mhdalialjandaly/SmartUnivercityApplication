using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Enrollment : IEntity
    {
        public Enrollment() 
        {
            Attendances = new HashSet<Attendance>();
            ExamResults = new HashSet<ExamResult>();
        }
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal? MidtermGrade { get; set; }
        public double? FinalGrade { get; set; }
        public double? ExamScore { get; set; }
        public EnrollmentStatus Status { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public decimal CourseFee { get; set; }
        public bool IsPaid { get; set; }

        // العلاقات
        public Student Student { get; set; }
        public Course Course { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        DateTime IEntity.DeletedAt { get; set; }
    }
}
