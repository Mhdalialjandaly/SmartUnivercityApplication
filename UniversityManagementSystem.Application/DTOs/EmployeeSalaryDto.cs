
namespace UniversityManagementSystem.Application.DTOs
{
    public class EmployeeSalaryDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePosition { get; set; }
        public DateTime SalaryDate { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public decimal Bonus { get; set; }
        public decimal NetSalary { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
