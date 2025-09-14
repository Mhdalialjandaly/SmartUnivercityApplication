using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class AccountingEntryDto
    {
        public int Id { get; set; }
        public string EntryNumber { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public EntryType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // نسخ بناءً على كائن موجود
        public AccountingEntryDto() { }

        public AccountingEntryDto(AccountingEntryDto other)
        {
            Id = other.Id;
            EntryNumber = other.EntryNumber;
            Description = other.Description;
            EntryDate = other.EntryDate;
            Type = other.Type;
            Amount = other.Amount;
            CreatedAt = other.CreatedAt;
            UpdatedAt = other.UpdatedAt;
        }
    }
}
