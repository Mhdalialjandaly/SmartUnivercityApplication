namespace UniversityManagementSystem.Domain.Entities
{
    public class StudentDocument
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string DocType { get; set; }
        public string SponsorEndorsement { get; set; }
        public DateTime CreatedDate { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
