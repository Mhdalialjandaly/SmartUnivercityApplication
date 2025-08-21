using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class ScheduledReport : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ReportType Type { get; set; }

        public int DayOfMonth { get; set; } // 1-31

        public TimeSpan Time { get; set; } // الوقت في اليوم

        public int? DepartmentId { get; set; }

        public bool IsActive { get; set; } = true;

        public TransactionType TransactionType { get; set; }

        public DateTime? LastRunDate { get; set; }

        public string? CreatedBy { get; set; }

        // Navigation Properties
        public virtual Department? Department { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
