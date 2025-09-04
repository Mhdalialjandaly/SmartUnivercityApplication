using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class ExamDto
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
        public CourseDto Course { get; set; }
        public IList<ExamResultDto> ExamResults { get; set; } = new List<ExamResultDto>();
        public IList<EnrollmentDto> Enrollments { get; set; } = new List<EnrollmentDto>();
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}
