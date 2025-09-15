
namespace UniversityManagementSystem.Domain.Entities
{
    public class AssetMaintenance : IEntity
    {
        public int Id { get; set; }
        public int FixedAssetId { get; set; }
        public string MaintenanceType { get; set; }
        public string Description { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public decimal Cost { get; set; }
        public string PerformedBy { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
