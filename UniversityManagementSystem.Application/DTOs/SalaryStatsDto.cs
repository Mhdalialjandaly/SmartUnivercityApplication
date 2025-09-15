namespace UniversityManagementSystem.Application.DTOs
{
    public class SalaryStatsDto
    {
        public int TotalEmployees { get; set; }
        public int ActiveSalaries { get; set; }
        public int PendingSalaries { get; set; }
        public decimal TotalMonthlySalary { get; set; }
        public decimal TotalPaidThisMonth { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal AverageSalary { get; set; }
        public int DepartmentsCount { get; set; }
        public decimal PaymentPercentage => TotalMonthlySalary > 0 ? (TotalPaidThisMonth / TotalMonthlySalary) * 100 : 0;
        public bool HasPendingSalaries => PendingSalaries > 0;
    }
}
