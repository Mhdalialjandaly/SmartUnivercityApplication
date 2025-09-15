
namespace UniversityManagementSystem.Application.DTOs
{
    public class AssetMaintenanceDto
    {
        public int Id { get; set; }
        public int FixedAssetId { get; set; }
        public string AssetName { get; set; }
        public string MaintenanceType { get; set; }
        public string Description { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public decimal Cost { get; set; }
        public string PerformedBy { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
