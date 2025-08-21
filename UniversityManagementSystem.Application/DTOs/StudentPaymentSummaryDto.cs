using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentPaymentSummaryDto
    {
        public int TotalPayments { get; set; }
        public decimal TotalAmount { get; set; }
        public int PendingPayments { get; set; }
        public decimal PendingAmount { get; set; }
        public int RejectedPayments { get; set; }
        public decimal RejectedAmount { get; set; }
        public decimal MonthlyAverage { get; set; }
    }
}
