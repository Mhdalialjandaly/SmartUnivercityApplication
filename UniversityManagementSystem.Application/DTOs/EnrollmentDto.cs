using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal? MidtermGrade { get; set; }
        public decimal? FinalGrade { get; set; }
        public decimal? ExamScore { get; set; }
        public EnrollmentStatus Status { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public decimal CourseFee { get; set; }
        public bool IsPaid { get; set; }

        // العلاقات
        public StudentDto Student { get; set; }
        public CourseDto Course { get; set; }
        public List<AttendanceDto> Attendances { get; set; } = new List<AttendanceDto>();
        public IList<ExamResultDto> ExamResults { get; set; } = new List<ExamResultDto>();
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}
