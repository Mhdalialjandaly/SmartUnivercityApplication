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
        public string Code { get; set; } 
        public string Category { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long Size { get; set; } = 0;
        public int FileCount { get; set; } = 0;
        public int DepartmentCount { get; set; } = 0;
        public int PageCount { get; set; } = 0;
        public string UnitType { get; set; } = "صفحة";
        public DepartmentDto Department { get; set; }

    }
}
