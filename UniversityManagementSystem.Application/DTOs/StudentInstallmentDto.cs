using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentInstallmentDto
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string Program { get; set; }
        public string AcademicYear { get; set; }
        public decimal Amount { get; set; }
        public decimal OutstandingAmount { get; set; }
        public decimal PaidAmount => Amount - OutstandingAmount;
        public DateTime DueDate { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsOverdue => DueDate < DateTime.Now && Status != PaymentStatus.Paid;
        public int DaysOverdue => IsOverdue ? (DateTime.Now - DueDate).Days : 0;
        public decimal PaymentPercentage => Amount > 0 ? (PaidAmount / Amount) * 100 : 0;
    }
}
