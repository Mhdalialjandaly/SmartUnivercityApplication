
namespace UniversityManagementSystem.Application.DTOs
{
    public class CourseRegistrationSummaryDto
    {
        public int TotalRegistrations { get; set; }
        public int ActiveRegistrations { get; set; }
        public int CancelledRegistrations { get; set; }
        public int CompletedRegistrations { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal OutstandingPayments { get; set; }
    }
}
