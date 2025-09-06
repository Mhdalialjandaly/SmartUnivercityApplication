using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class AssessmentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; } 
        public int TotalStudents { get; set; }
        public int CompletedCount { get; set; }
        public int PendingCount { get; set; }
        public AssessmentType Type { get; set; }
        public AssessmentStatus Status { get; set; }
        public int? DepartmentId { get; set; }
        public DepartmentDto Department { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}