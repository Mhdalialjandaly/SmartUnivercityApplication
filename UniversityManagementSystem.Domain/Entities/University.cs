using Microsoft.EntityFrameworkCore.Metadata;

namespace UniversityManagementSystem.Domain.Entities
{
    public class University:IEntity
    {
        public  University() {
        Departments = new HashSet<Department>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}
