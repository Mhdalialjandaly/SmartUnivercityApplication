using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class InstallmentPaymentDto
    {
        public int Id { get; set; }
        public int StudentInstallmentId { get; set; }
        public string StudentName { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; }
        public string ReceiptNumber { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
