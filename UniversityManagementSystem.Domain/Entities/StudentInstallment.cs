using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class StudentInstallment : IEntity
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string Program { get; set; }
        public string AcademicYear { get; set; }
        public decimal Amount { get; set; }
        public decimal OutstandingAmount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
