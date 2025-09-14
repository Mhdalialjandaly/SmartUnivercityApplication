

namespace UniversityManagementSystem.Application.DTOs
{
    public class AccountingStatsDto
    {
        public int TotalEntries { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal NetBalance { get; set; }
        public int RecentEntriesCount { get; set; }
    }
}
