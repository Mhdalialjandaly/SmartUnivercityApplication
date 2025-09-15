using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<PaginatedResult<InventoryItemDto>> GetInventoryItemsAsync(int pageNumber, int pageSize, string searchTerm = "", string category = "", InventoryStatus? status = null);
        Task<List<InventoryItemDto>> GetAllInventoryItemsAsync();
        Task<InventoryItemDto> GetInventoryItemByIdAsync(int itemId);
        Task<InventoryItemDto> AddInventoryItemAsync(InventoryItemDto itemDto);
        Task UpdateInventoryItemAsync(int itemId, InventoryItemDto itemDto);
        Task DeleteInventoryItemAsync(int itemId);
        Task<InventoryStatsDto> GetInventoryStatsAsync();
        Task<List<InventoryItemDto>> GetLowStockItemsAsync(int threshold);
        Task<List<InventoryItemDto>> GetOutOfStockItemsAsync();
        Task<List<InventoryMovementDto>> GetItemMovementsAsync(int itemId);
        Task<InventoryMovementDto> AddItemMovementAsync(InventoryMovementDto movementDto);
        Task<byte[]> ExportInventoryToExcelAsync();
        Task<byte[]> ExportInventoryToPdfAsync();
        Task<List<InventoryCategoryDto>> GetInventoryCategoriesAsync();
        Task<InventoryItemDto> UpdateItemQuantityAsync(int itemId, int quantity, string reason);
        Task<List<InventoryItemDto>> GetItemsByCategoryAsync(string category);
        Task<decimal> GetTotalInventoryValueAsync();
    }
}
