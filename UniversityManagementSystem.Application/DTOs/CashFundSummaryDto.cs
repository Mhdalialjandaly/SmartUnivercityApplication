namespace UniversityManagementSystem.Application.DTOs
{
    public class CashFundSummaryDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalDeposits { get; set; }
        public decimal TotalWithdrawals { get; set; }
        public decimal NetChange { get; set; }
        public int TransactionCount { get; set; }
        public decimal AverageTransactionAmount { get; set; }
        public bool IsProfitable => NetChange > 0;
    }
}
