namespace UniversityManagementSystem.Domain.Entities
{
    public class Tunnel
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
    }
}
