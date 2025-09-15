
namespace UniversityManagementSystem.Application.DTOs
{
    public class SalaryDeductionDto
    {
        public int Id { get; set; }
        public int EmployeeSalaryId { get; set; }
        public string EmployeeName { get; set; }
        public string DeductionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime DeductionDate { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
