
namespace UniversityManagementSystem.Application.DTOs
{
    public class TrialBalanceDto
    {
        public List<TrialBalanceAccountDto> Accounts { get; set; } = new List<TrialBalanceAccountDto>();
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime GeneratedAt { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal Difference { get; set; }
        public bool IsBalanced { get; set; }
    }
}
