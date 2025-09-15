using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IFixedAssetsService
    {
        Task<PaginatedResult<FixedAssetDto>> GetFixedAssetsAsync(int pageNumber, int pageSize, string searchTerm = "", string category = "", AssetStatus? status = null);
        Task<List<FixedAssetDto>> GetAllFixedAssetsAsync();
        Task<FixedAssetDto> GetFixedAssetByIdAsync(int assetId);
        Task<FixedAssetDto> AddFixedAssetAsync(FixedAssetDto assetDto);
        Task UpdateFixedAssetAsync(int assetId, FixedAssetDto assetDto);
        Task DeleteFixedAssetAsync(int assetId);
        Task<FixedAssetsStatsDto> GetFixedAssetsStatsAsync();
        Task<List<FixedAssetDto>> GetAssetsByCategoryAsync(string category);
        Task<List<FixedAssetDto>> GetDepreciatedAssetsAsync();
        Task<List<FixedAssetDto>> GetAssetsNeedingMaintenanceAsync();
        Task<List<AssetMaintenanceDto>> GetAssetMaintenanceHistoryAsync(int assetId);
        Task<AssetMaintenanceDto> AddAssetMaintenanceAsync(AssetMaintenanceDto maintenanceDto);
        Task<byte[]> ExportFixedAssetsToExcelAsync();
        Task<byte[]> ExportFixedAssetsToPdfAsync();
        Task<List<AssetCategoryDto>> GetAssetCategoriesAsync();
        Task<decimal> GetTotalAssetValueAsync();
        Task<decimal> GetDepreciatedValueAsync();
        Task<List<FixedAssetDto>> GetAssetsByDepartmentAsync(string department);
        Task<FixedAssetDto> AssignAssetToDepartmentAsync(int assetId, string department);
    }
}
