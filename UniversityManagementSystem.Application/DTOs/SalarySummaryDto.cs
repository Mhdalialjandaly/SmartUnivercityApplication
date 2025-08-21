
namespace UniversityManagementSystem.Application.DTOs
{
    public class SalarySummaryDto
    {
        public int TotalSalaries { get; set; }
        public decimal TotalAmount { get; set; }
        public int PendingSalaries { get; set; }
        public decimal PendingAmount { get; set; }
        public int RejectedSalaries { get; set; }
        public decimal RejectedAmount { get; set; }
        public decimal AverageSalary { get; set; }
    }
}
