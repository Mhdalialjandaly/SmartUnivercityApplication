
namespace UniversityManagementSystem.Domain.Entities
{
    public class FinanceRecord : IEntity
    {
        public int Id { get; set; }
        public string Type { get; set; } 
        public string Description { get; set; }
        public string  Category { get; set; }
        public decimal Amount { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
