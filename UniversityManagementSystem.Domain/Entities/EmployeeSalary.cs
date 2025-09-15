using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class EmployeeSalary : IEntity
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetSalary { get; set; }
        public SalaryStatus Status { get; set; }
        public string BankAccount { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
