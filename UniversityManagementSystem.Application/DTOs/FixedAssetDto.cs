using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class FixedAssetDto
    {
        public int Id { get; set; }
        public string AssetNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal DepreciationRate { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
        public AssetStatus Status { get; set; }
        public string SerialNumber { get; set; }
        public string Supplier { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal DepreciationAmount => PurchasePrice - CurrentValue;
        public int AgeInYears => (int)((DateTime.Now - PurchaseDate).TotalDays / 365.25);
    }
}
