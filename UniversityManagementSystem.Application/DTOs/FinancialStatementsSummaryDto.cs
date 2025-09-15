
namespace UniversityManagementSystem.Application.DTOs
{
    public class FinancialStatementsSummaryDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime GeneratedAt { get; set; }
        public decimal NetIncome { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }
        public decimal TotalAssets { get; set; }
        public decimal TotalLiabilities { get; set; }
        public decimal TotalEquity { get; set; }
        public decimal NetCashFlow { get; set; }
        public bool IsProfitable => NetIncome > 0;
        public decimal EquityRatio => TotalAssets > 0 ? (TotalEquity / TotalAssets) * 100 : 0;
    }
}
