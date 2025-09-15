namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentInstallmentStatsDto
    {
        public int TotalStudents { get; set; }
        public int TotalInstallments { get; set; }
        public int PaidInstallments { get; set; }
        public int PendingInstallments { get; set; }
        public int OverdueInstallments { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public decimal OutstandingAmount { get; set; }
        public int ProgramsCount { get; set; }
        public decimal CollectionRate => TotalAmount > 0 ? (CollectedAmount / TotalAmount) * 100 : 0;
        public decimal OverdueRate => TotalInstallments > 0 ? ((decimal)OverdueInstallments / TotalInstallments) * 100 : 0;
        public bool HasOverdue => OverdueInstallments > 0;
    }
}
