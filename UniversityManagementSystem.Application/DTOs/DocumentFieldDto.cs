
namespace UniversityManagementSystem.Application.DTOs
{
    public class DocumentFieldDto
    {
        public int Id { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string FieldType { get; set; }
        public int DocumentId { get; set; }
    }
}
