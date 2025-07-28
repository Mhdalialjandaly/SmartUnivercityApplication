
namespace UniversityManagementSystem.Application.DTOs
{
    public class LeaveSummaryDto
    {
        public int TotalLeaves { get; set; }
        public int PendingLeaves { get; set; }
        public int ApprovedLeaves { get; set; }
        public int RejectedLeaves { get; set; }
        public int TotalLeaveDays { get; set; }
    }
}
