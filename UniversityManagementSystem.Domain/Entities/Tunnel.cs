
namespace UniversityManagementSystem.Domain.Entities
{
    public class Tunnel:IEntity
    {
        public Tunnel() {
        Students = new HashSet<Student>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeOfkinship { get; set; }
        public string FirstPart { get; set; }
        public string SecoundPart { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}
