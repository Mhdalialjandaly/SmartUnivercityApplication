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
        public ArchiveType Type { get; set; }
        public string Status { get; set; } 
        public DateTime UploadDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? DepartmentId { get; set; }
        public string Tags { get; set; } 
        public string UploadedBy { get; set; }
        public Department Department { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
