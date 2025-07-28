using System;

namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentPaymentDto
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public string StudentName { get; set; } = "";
        public string CourseName { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = "";
        public string TransactionId { get; set; } = "";
        public string Status { get; set; } = "";
        public string Notes { get; set; } = "";
    }

    public class StudentPaymentSummaryDto
    {
        public int TotalPayments { get; set; }
        public decimal TotalAmount { get; set; }
        public int PendingPayments { get; set; }
        public decimal PendingAmount { get; set; }
    }
}
