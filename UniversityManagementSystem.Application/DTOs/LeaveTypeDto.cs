
namespace UniversityManagementSystem.Application.DTOs
{
    public class LeaveTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int MaxDaysPerYear { get; set; }
        public bool IsPaid { get; set; } = true;
        public bool IsActive { get; set; } = true;
    }
}
