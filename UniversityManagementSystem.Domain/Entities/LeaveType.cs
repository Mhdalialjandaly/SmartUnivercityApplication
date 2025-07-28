using System.ComponentModel.DataAnnotations;
namespace UniversityManagementSystem.Domain.Entities
{
    public class LeaveType :IEntity  
    {
        public LeaveType() 
        {
            Leaves = new HashSet<Leave>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } 

        [StringLength(200)]
        public string Description { get; set; } 

        public int MaxDaysPerYear { get; set; }

        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }

        // العلاقات
        public virtual ICollection<Leave> Leaves { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
