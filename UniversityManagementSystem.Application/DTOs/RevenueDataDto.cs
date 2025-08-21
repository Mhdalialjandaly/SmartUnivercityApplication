
namespace UniversityManagementSystem.Application.DTOs
{
    public class RevenueDataDto
    {
        public decimal Total { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<FinanceRecordDto> RevenueRecords { get; set; }
        public List<StudentPaymentDto> PaymentRecords { get; set; }
    }
}
