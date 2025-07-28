
namespace UniversityManagementSystem.Application.DTOs
{
    public class LeaveDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } 
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays { get; set; }
        public string Reason { get; set; } 
        public string Status { get; set; } 
        public string Notes { get; set; } 
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; } 
    }
}
