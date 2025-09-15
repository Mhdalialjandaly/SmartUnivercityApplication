

namespace UniversityManagementSystem.Application.DTOs
{
    public class EquityStatementItemDto
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public decimal BeginningBalance { get; set; }
        public decimal EndingBalance { get; set; }
        public decimal Change { get; set; }
    }
}
