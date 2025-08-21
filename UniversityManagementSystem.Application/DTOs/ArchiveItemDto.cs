using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class ArchiveItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ArchiveType DocumentType { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadedBy { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Status { get; set; }
        public DepartmentDto Department { get; set; }

    }
}
