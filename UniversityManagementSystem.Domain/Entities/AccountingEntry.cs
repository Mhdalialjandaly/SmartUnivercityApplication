using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class AccountingEntry : IEntity
    {
        public int Id { get; set; }
        public string EntryNumber { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public EntryType Type { get; set; }
        public decimal Amount { get; set; }
        public string AccountCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
