using System;

namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentPaymentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } // نقداً, تحويل بنكي, بطاقة ائتمان, شيك
        public string Status { get; set; } // مدفوع, معلق, مرفوض
        public string TransactionId { get; set; }
        public string Notes { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

}
