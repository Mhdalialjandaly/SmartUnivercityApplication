using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class ArchiveItem : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public ArchiveType DocumentType { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string FileType { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadedBy { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public DateTime CreatedDate { get; set; } 
        public long Size { get; set; } 
        public int FileCount { get; set; } 
        public int DepartmentCount { get; set; } 
        public int PageCount { get; set; } 
        public string UnitType { get; set; }
        public ArchiveType Type { get; set; }
        public string Tags { get; set; } 
        public Department Department { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
