namespace UniversityManagementSystem.Application.DTOs
{
    public class FixedAssetsStatsDto
    {
        public int TotalAssets { get; set; }
        public int ActiveAssets { get; set; }
        public int RetiredAssets { get; set; }
        public int UnderMaintenanceAssets { get; set; }
        public decimal TotalOriginalValue { get; set; }
        public decimal TotalCurrentValue { get; set; }
        public decimal TotalDepreciation { get; set; }
        public int AssetsNeedingMaintenance { get; set; }
        public int RecentMaintenance { get; set; }
        public int CategoriesCount { get; set; }
        public decimal DepreciationPercentage => TotalOriginalValue > 0 ? (TotalDepreciation / TotalOriginalValue) * 100 : 0;
        public bool HasMaintenanceDue => AssetsNeedingMaintenance > 0;
    }
}
