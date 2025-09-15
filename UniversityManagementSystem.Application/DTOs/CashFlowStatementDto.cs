
namespace UniversityManagementSystem.Application.DTOs
{
    public class CashFlowStatementDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime GeneratedAt { get; set; }
        public List<FinancialStatementItemDto> OperatingItems { get; set; } = new List<FinancialStatementItemDto>();
        public decimal NetOperatingCashFlow { get; set; }
        public List<FinancialStatementItemDto> InvestingItems { get; set; } = new List<FinancialStatementItemDto>();
        public decimal NetInvestingCashFlow { get; set; }
        public List<FinancialStatementItemDto> FinancingItems { get; set; } = new List<FinancialStatementItemDto>();
        public decimal NetFinancingCashFlow { get; set; }
        public decimal NetChangeInCash { get; set; }
    }
}
