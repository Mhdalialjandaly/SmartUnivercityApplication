
namespace UniversityManagementSystem.Application.DTOs
{
    public class FinanceRecordDto
    {
        public int Id { get; set; }
        public string Type { get; set; } // دخل أو مصاريف
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}
