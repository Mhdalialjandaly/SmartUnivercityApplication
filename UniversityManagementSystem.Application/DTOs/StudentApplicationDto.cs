

using System.ComponentModel.DataAnnotations;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentApplicationDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string EducationLevel { get; set; }

        [Range(0, 100)]
        public decimal GPA { get; set; }

        public string CertificatePath { get; set; }

        public DateTime SubmitDate { get; set; } 

        [StringLength(20)]
        public string Status { get; set; } 

        public string RejectionReason { get; set; }
        public string StudentId { get; set; }

        public string TemporaryPassword { get; set; }
        public int NationalityId { get; set; }
        public string Religion { get; set; }
        // العلاقات
        public DepartmentDto Department { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
