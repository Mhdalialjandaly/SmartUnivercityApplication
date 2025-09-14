
namespace UniversityManagementSystem.Domain.Entities
{
    public class Account : IEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string AccountType { get; set; } 
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
