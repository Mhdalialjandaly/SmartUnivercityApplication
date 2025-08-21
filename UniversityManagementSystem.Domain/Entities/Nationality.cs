namespace UniversityManagementSystem.Domain.Entities
{
    public class Nationality : IEntity
    {
        public Nationality()
        {
            Students = new HashSet<Student>();
        }
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }

       
        public ICollection<Student> Students { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}