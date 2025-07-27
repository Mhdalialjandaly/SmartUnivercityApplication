
namespace UniversityManagementSystem.Application.DTOs
{
    public class DocumentSignatureDto
    {
        public int Id { get; set; }
        public string SignerName { get; set; } = "";
        public string SignerPosition { get; set; } = "";
        public string SignatureData { get; set; } = "";
        public DateTime SignatureDate { get; set; }
        public string SignatureType { get; set; } = "";
        public int DocumentId { get; set; }
    }
}
