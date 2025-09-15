using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class CashTransaction : IEntity
    {
        public int Id { get; set; }
        public string TransactionNumber { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string ReferenceNumber { get; set; }
        public string Beneficiary { get; set; }
        public TransactionStatus Status { get; set; }
        public int CashFundId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
