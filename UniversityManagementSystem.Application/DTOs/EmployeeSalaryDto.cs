
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class EmployeeSalaryDto
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
        public decimal GrossSalary => BaseSalary + Allowances;
        public decimal DeductionPercentage => GrossSalary > 0 ? (TotalDeductions / GrossSalary) * 100 : 0;
    }
}
