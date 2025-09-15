using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Enums;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class FixedAssetsService : Injectable, IFixedAssetsService
    {
        private readonly IRepository<FixedAsset> _assetRepository;
        private readonly IRepository<AssetMaintenance> _maintenanceRepository;
        private readonly IRepository<AssetCategory> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FixedAssetsService(
            IRepository<FixedAsset> assetRepository,
            IRepository<AssetMaintenance> maintenanceRepository,
            IRepository<AssetCategory> categoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _assetRepository = assetRepository;
            _maintenanceRepository = maintenanceRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<FixedAssetDto>> GetFixedAssetsAsync(int pageNumber, int pageSize, string searchTerm = "", string category = "", AssetStatus? status = null)
        {
            var assets = await _assetRepository.GetAllAsync();
            var query = assets.AsQueryable();

            // تطبيق الفلاتر
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a => a.Name.Contains(searchTerm) ||
                                      a.AssetNumber.Contains(searchTerm) ||
                                      a.Description.Contains(searchTerm) ||
                                      a.Department.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(a => a.Category == category);
            }

            if (status.HasValue)
            {
                query = query.Where(a => a.Status == status.Value);
            }

            var filteredAssets = query.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            var totalCount = query.Count();

            return new PaginatedResult<FixedAssetDto>
            {
                Data = _mapper.Map<List<FixedAssetDto>>(filteredAssets),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<List<FixedAssetDto>> GetAllFixedAssetsAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            return _mapper.Map<List<FixedAssetDto>>(assets.OrderBy(a => a.Name));
        }

        public async Task<FixedAssetDto> GetFixedAssetByIdAsync(int assetId)
        {
            var asset = await _assetRepository.GetByIdAsync(assetId);
            if (asset == null)
                throw new Exception("الأصل الثابت غير موجود");

            return _mapper.Map<FixedAssetDto>(asset);
        }

        public async Task<FixedAssetDto> AddFixedAssetAsync(FixedAssetDto assetDto)
        {
            // التحقق من صحة البيانات
            if (string.IsNullOrWhiteSpace(assetDto.Name))
                throw new Exception("اسم الأصل الثابت مطلوب");

            if (string.IsNullOrWhiteSpace(assetDto.AssetNumber))
                throw new Exception("رقم الأصل الثابت مطلوب");

            if (assetDto.PurchasePrice <= 0)
                throw new Exception("سعر الشراء يجب أن يكون أكبر من صفر");

            var asset = _mapper.Map<FixedAsset>(assetDto);
            asset.CreatedAt = DateTime.Now;
            asset.UpdatedAt = DateTime.Now;
            asset.CurrentValue = CalculateCurrentValue(asset);

            await _assetRepository.AddAsync(asset);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<FixedAssetDto>(asset);
        }

        public async Task UpdateFixedAssetAsync(int assetId, FixedAssetDto assetDto)
        {
            var existingAsset = await _assetRepository.GetByIdAsync(assetId);
            if (existingAsset == null)
                throw new Exception("الأصل الثابت غير موجود");

            _mapper.Map(assetDto, existingAsset);
            existingAsset.UpdatedAt = DateTime.Now;
            existingAsset.CurrentValue = CalculateCurrentValue(existingAsset);

            _assetRepository.Update(existingAsset);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteFixedAssetAsync(int assetId)
        {
            var asset = await _assetRepository.GetByIdAsync(assetId);
            if (asset == null)
                throw new Exception("الأصل الثابت غير موجود");

            _assetRepository.Delete(asset);
            await _unitOfWork.CommitAsync();
        }

        public async Task<FixedAssetsStatsDto> GetFixedAssetsStatsAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            var maintenanceRecords = await _maintenanceRepository.GetAllAsync();

            var totalAssets = assets.Count();
            var activeAssets = assets.Count(a => a.Status == AssetStatus.Active);
            var retiredAssets = assets.Count(a => a.Status == AssetStatus.Retired);
            var underMaintenanceAssets = assets.Count(a => a.Status == AssetStatus.UnderMaintenance);

            var totalOriginalValue = assets.Sum(a => a.PurchasePrice);
            var totalCurrentValue = assets.Sum(a => CalculateCurrentValue(a));
            var totalDepreciation = totalOriginalValue - totalCurrentValue;

            var assetsNeedingMaintenance = assets.Count(a => a.NextMaintenanceDate <= DateTime.Now.AddDays(30));
            var recentMaintenance = maintenanceRecords.Count(m => m.MaintenanceDate >= DateTime.Now.AddDays(-30));

            return new FixedAssetsStatsDto
            {
                TotalAssets = totalAssets,
                ActiveAssets = activeAssets,
                RetiredAssets = retiredAssets,
                UnderMaintenanceAssets = underMaintenanceAssets,
                TotalOriginalValue = totalOriginalValue,
                TotalCurrentValue = totalCurrentValue,
                TotalDepreciation = totalDepreciation,
                AssetsNeedingMaintenance = assetsNeedingMaintenance,
                RecentMaintenance = recentMaintenance,
                CategoriesCount = (await _categoryRepository.GetAllAsync()).Count()
            };
        }

        public async Task<List<FixedAssetDto>> GetAssetsByCategoryAsync(string category)
        {
            var assets = await _assetRepository.GetAllAsync();
            var categoryAssets = assets.Where(a => a.Category == category)
                                     .OrderBy(a => a.Name)
                                     .ToList();

            return _mapper.Map<List<FixedAssetDto>>(categoryAssets);
        }

        public async Task<List<FixedAssetDto>> GetDepreciatedAssetsAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            var depreciatedAssets = assets.Where(a => CalculateCurrentValue(a) < a.PurchasePrice * 0.5m)
                                        .OrderBy(a => CalculateCurrentValue(a))
                                        .ToList();

            return _mapper.Map<List<FixedAssetDto>>(depreciatedAssets);
        }

        public async Task<List<FixedAssetDto>> GetAssetsNeedingMaintenanceAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            var maintenanceAssets = assets.Where(a => a.NextMaintenanceDate <= DateTime.Now.AddDays(30) &&
                                                   a.Status != AssetStatus.Retired)
                                        .OrderBy(a => a.NextMaintenanceDate)
                                        .ToList();

            return _mapper.Map<List<FixedAssetDto>>(maintenanceAssets);
        }

        public async Task<List<AssetMaintenanceDto>> GetAssetMaintenanceHistoryAsync(int assetId)
        {
            var maintenanceRecords = await _maintenanceRepository.GetAllAsync();
            var assetMaintenance = maintenanceRecords.Where(m => m.FixedAssetId == assetId)
                                                   .OrderByDescending(m => m.MaintenanceDate)
                                                   .ToList();

            return _mapper.Map<List<AssetMaintenanceDto>>(assetMaintenance);
        }

        public async Task<AssetMaintenanceDto> AddAssetMaintenanceAsync(AssetMaintenanceDto maintenanceDto)
        {
            var maintenance = _mapper.Map<AssetMaintenance>(maintenanceDto);
            maintenance.MaintenanceDate = DateTime.Now;
            maintenance.CreatedAt = DateTime.Now;

            await _maintenanceRepository.AddAsync(maintenance);
            await _unitOfWork.CommitAsync();

            // تحديث حالة الأصل الثابت
            var asset = await _assetRepository.GetByIdAsync(maintenanceDto.FixedAssetId);
            if (asset != null)
            {
                asset.LastMaintenanceDate = maintenance.MaintenanceDate;
                asset.NextMaintenanceDate = maintenance.NextMaintenanceDate;
                asset.Status = AssetStatus.Active;
                asset.UpdatedAt = DateTime.Now;

                _assetRepository.Update(asset);
                await _unitOfWork.CommitAsync();
            }

            return _mapper.Map<AssetMaintenanceDto>(maintenance);
        }

        public async Task<byte[]> ExportFixedAssetsToExcelAsync()
        {
            var assets = await GetAllFixedAssetsAsync();
            // تنفيذ التصدير إلى Excel
            return new byte[0];
        }

        public async Task<byte[]> ExportFixedAssetsToPdfAsync()
        {
            var stats = await GetFixedAssetsStatsAsync();
            var assets = await GetAllFixedAssetsAsync();
            // تنفيذ التصدير إلى PDF
            return new byte[0];
        }

        public async Task<List<AssetCategoryDto>> GetAssetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<AssetCategoryDto>>(categories.OrderBy(c => c.Name));
        }

        public async Task<decimal> GetTotalAssetValueAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            return assets.Sum(a => CalculateCurrentValue(a));
        }

        public async Task<decimal> GetDepreciatedValueAsync()
        {
            var assets = await _assetRepository.GetAllAsync();
            var originalValue = assets.Sum(a => a.PurchasePrice);
            var currentValue = assets.Sum(a => CalculateCurrentValue(a));
            return originalValue - currentValue;
        }

        public async Task<List<FixedAssetDto>> GetAssetsByDepartmentAsync(string department)
        {
            var assets = await _assetRepository.GetAllAsync();
            var departmentAssets = assets.Where(a => a.Department == department)
                                       .OrderBy(a => a.Name)
                                       .ToList();

            return _mapper.Map<List<FixedAssetDto>>(departmentAssets);
        }

        public async Task<FixedAssetDto> AssignAssetToDepartmentAsync(int assetId, string department)
        {
            var asset = await _assetRepository.GetByIdAsync(assetId);
            if (asset == null)
                throw new Exception("الأصل الثابت غير موجود");

            asset.Department = department;
            asset.UpdatedAt = DateTime.Now;

            _assetRepository.Update(asset);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<FixedAssetDto>(asset);
        }

        private decimal CalculateCurrentValue(FixedAsset asset)
        {
            if (asset.Status == AssetStatus.Retired)
                return 0;

            var yearsSincePurchase = (DateTime.Now - asset.PurchaseDate).TotalDays / 365.25;
            var depreciationRate = asset.DepreciationRate / 100;
            var depreciation = asset.PurchasePrice * (decimal)(yearsSincePurchase * (double)depreciationRate);

            var currentValue = asset.PurchasePrice - depreciation;
            return currentValue > 0 ? currentValue : 0;
        }
    }
}
