

namespace UniversityManagementSystem.Application.DTOs
{
    public class BalanceSheetDto
    {
        public DateTime AsOfDate { get; set; }
        public DateTime GeneratedAt { get; set; }
        public List<FinancialStatementItemDto> AssetItems { get; set; } = new List<FinancialStatementItemDto>();
        public decimal TotalAssets { get; set; }
        public List<FinancialStatementItemDto> LiabilityItems { get; set; } = new List<FinancialStatementItemDto>();
        public decimal TotalLiabilities { get; set; }
        public List<FinancialStatementItemDto> EquityItems { get; set; } = new List<FinancialStatementItemDto>();
        public decimal TotalEquity { get; set; }
        public decimal TotalLiabilitiesAndEquity { get; set; }
        public bool IsBalanced => Math.Abs(TotalAssets - TotalLiabilitiesAndEquity) < 0.01m;
    }
}
