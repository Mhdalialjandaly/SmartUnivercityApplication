using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.DTOs
{
    public class InvoiceDto
    {
       

        public int Id { get; set; } 

        [Display(Name = "رقم الفاتورة")]
        public string InvoiceNumber { get; set; } = string.Empty; // e.g., "INV-2023-001"

        [Display(Name = "التاريخ")]
        public DateTime Date { get; set; } = DateTime.Now; 

        [Display(Name = "الوصف")]
        public string Description { get; set; } = string.Empty; 

        [Display(Name = "المبلغ")]
        public decimal Amount { get; set; } 

        [Display(Name = "الحالة")]
        public string Status { get; set; } = "معلقة"; // e.g., "مدفوعة", "متأخرة", "معلقة", "ملغاة"

        public string StudentId { get; set; } = string.Empty;

        public DateTime DueDate { get; set; } // When the payment is due
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal Tax { get; set; } // Tax amount if applicable
        public decimal Discount { get; set; } // Discount amount if applicable
        public string Notes { get; set; } = string.Empty; // Additional notes
    }
}
