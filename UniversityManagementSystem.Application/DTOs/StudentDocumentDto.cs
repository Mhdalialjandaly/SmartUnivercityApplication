
namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentDocumentDto
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string DocType { get; set; }
        public string SponsorEndorsement { get; set; }
        public DateTime CreatedDate { get; set; }
        public int StudentId { get; set; }
        public StudentDto Student { get; set; }
    }
}
