
namespace UniversityManagementSystem.Application.DTOs
{
    public class InventoryStatsDto
    {
        public int TotalItems { get; set; }
        public int InStockItems { get; set; }
        public int LowStockItems { get; set; }
        public int OutOfStockItems { get; set; }
        public decimal TotalValue { get; set; }
        public int RecentMovements { get; set; }
        public int CategoriesCount { get; set; }
        public double StockoutPercentage => TotalItems > 0 ? (double)OutOfStockItems / TotalItems * 100 : 0;
        public bool HasLowStock => LowStockItems > 0;
    }
}
