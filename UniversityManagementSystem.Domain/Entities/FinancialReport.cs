using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class FinancialReport : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; }

        public ReportType Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ReportStatus Status { get; set; }

        public int? DepartmentId { get; set; }

        public TransactionType TransactionType { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public string FilePath { get; set; }

        public DateTime GeneratedDate { get; set; }

        public string GeneratedBy { get; set; }

        // Navigation Properties
        public virtual Department Department { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
