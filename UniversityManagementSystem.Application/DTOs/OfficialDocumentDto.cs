
namespace UniversityManagementSystem.Application.DTOs
{
    public class OfficialDocumentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; } 
        public string DocumentType { get; set; } 
        public string TemplateName { get; set; } 
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime? IssuedDate { get; set; }
        public string IssuedTo { get; set; } 
        public string DocumentNumber { get; set; } 
        public string SignatureData { get; set; } 
        public string Status { get; set; } 
        public string FilePath { get; set; }
        public List<DocumentFieldDto> DocumentFields { get; set; } = new List<DocumentFieldDto>();
        public List<DocumentSignatureDto> DocumentSignatures { get; set; } = new List<DocumentSignatureDto>();
    }
}
