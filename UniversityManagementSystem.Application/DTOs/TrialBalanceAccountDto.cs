namespace UniversityManagementSystem.Application.DTOs
{
    public class TrialBalanceAccountDto
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal Balance { get; set; }
        public bool IsDebitBalance => Balance > 0;
        public bool IsCreditBalance => Balance < 0;
    }
}
