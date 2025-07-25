using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Domain.Entities
{
    public class StudentApplication
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = "";

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = "";

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Address { get; set; } = "";

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string EducationLevel { get; set; } = "";

        [Range(0, 100)]
        public decimal GPA { get; set; }

        public string CertificatePath { get; set; } = "";

        public DateTime SubmitDate { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string Status { get; set; } = "قيد المراجعة"; // قيد المراجعة, مقبول, مرفوض, مكتمل

        public string RejectionReason { get; set; }

        public string StudentId { get; set; }

        public string TemporaryPassword { get; set; }

        // العلاقات
        public Department Department { get; set; }
        public Student Student { get; set; }
    }
}
