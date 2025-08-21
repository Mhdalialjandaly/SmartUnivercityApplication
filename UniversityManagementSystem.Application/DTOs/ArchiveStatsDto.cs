
namespace UniversityManagementSystem.Application.DTOs
{
    public class ArchiveStatsDto
    {
        public int TotalDocuments { get; set; }
        public int ActiveDocuments { get; set; }
        public int ExpiredDocuments { get; set; }
        public long TotalStorageUsed { get; set; } // بالميغابايت
    }
}
