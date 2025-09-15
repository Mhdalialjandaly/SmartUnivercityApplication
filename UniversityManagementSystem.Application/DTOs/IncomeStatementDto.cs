
namespace UniversityManagementSystem.Application.DTOs
{
    public class IncomeStatementDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime GeneratedAt { get; set; }
        public List<FinancialStatementItemDto> RevenueItems { get; set; } = new List<FinancialStatementItemDto>();
        public decimal TotalRevenue { get; set; }
        public List<FinancialStatementItemDto> ExpenseItems { get; set; } = new List<FinancialStatementItemDto>();
        public decimal TotalExpenses { get; set; }
        public decimal NetIncome { get; set; }
        public bool IsProfit => NetIncome > 0;
        public bool IsLoss => NetIncome < 0;
    }
}
