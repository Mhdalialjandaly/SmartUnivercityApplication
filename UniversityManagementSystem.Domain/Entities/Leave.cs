using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Leave : IEntity
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int LeaveTypeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int TotalDays { get; set; }

        [StringLength(500)]
        public string Reason { get; set; } 

        [StringLength(20)]
        public string Status { get; set; } 

        [StringLength(200)]
        public string Notes { get; set; } 

        public DateTime ApprovedDate { get; set; }

        public string ApprovedBy { get; set; }

        // العلاقات
        public virtual User Employee { get; set; }
        public virtual LeaveType LeaveType { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
