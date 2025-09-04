
namespace UniversityManagementSystem.Application.DTOs
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; } 
        public DateTime Date { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public decimal HoursWorked { get; set; }
        public string Status { get; set; } 
        public string Notes { get; set; }
        public int StudentId { get; set; }
        public bool IsPresent { get; set; }

    }
}
