namespace UniversityManagementSystem.Application.DTOs
{
    public class FinancialDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public decimal RevenuePercentage { get; set; }
    }
}
