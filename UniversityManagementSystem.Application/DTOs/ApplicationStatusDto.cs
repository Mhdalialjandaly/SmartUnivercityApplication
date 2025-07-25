
namespace UniversityManagementSystem.Application.DTOs
{
    public class ApplicationStatusDto
    {
        public string Id { get; set; } = "";
        public string StudentName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Department { get; set; } = "";
        public DateTime SubmitDate { get; set; }
        public string Status { get; set; } = "قيد المراجعة";
        public string StudentId { get; set; }
        public string TemporaryPassword { get; set; }
        public string RejectionReason { get; set; }
    }
}
