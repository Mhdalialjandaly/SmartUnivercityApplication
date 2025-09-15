using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class CashFundDto
    {
        public decimal CurrentBalance { get; set; }
        public int PendingTransactionsCount { get; set; }
        public int TodayTransactionsCount { get; set; }
        public DateTime LastUpdated { get; set; }
        public FundStatus Status { get; set; }
        public bool IsOverdrawn => CurrentBalance < 0;
        public string StatusText => Status == FundStatus.Active ? "نشط" : "مفرط";
    }
}
