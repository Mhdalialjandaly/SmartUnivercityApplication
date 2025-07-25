

namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentApplicationDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public int DepartmentId { get; set; }
        public string EducationLevel { get; set; } = "";
        public decimal GPA { get; set; }
        public string CertificatePath { get; set; } = "";
        public DateTime SubmitDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "قيد المراجعة";
        public string RejectionReason { get; set; }
        public string StudentId { get; set; }
        public string TemporaryPassword { get; set; }
    }
}
