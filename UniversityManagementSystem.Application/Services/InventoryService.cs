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
    public class InventoryService : Injectable, IInventoryService
    {
        private readonly IRepository<InventoryItem> _itemRepository;
        private readonly IRepository<InventoryMovement> _movementRepository;
        private readonly IRepository<InventoryCategory> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(
            IRepository<InventoryItem> itemRepository,
            IRepository<InventoryMovement> movementRepository,
            IRepository<InventoryCategory> categoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _movementRepository = movementRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<InventoryItemDto>> GetInventoryItemsAsync(int pageNumber, int pageSize, string searchTerm = "", string category = "", InventoryStatus? status = null)
        {
            var items = await _itemRepository.GetAllAsync();
            var query = items.AsQueryable();

            // تطبيق الفلاتر
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(i => i.Name.Contains(searchTerm) ||
                                       i.Code.Contains(searchTerm) ||
                                       i.Description.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(i => i.Category == category);
            }

            if (status.HasValue)
            {
                query = query.Where(i => i.Status == status.Value);
            }

            var filteredItems = query.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToList();

            var totalCount = query.Count();

            return new PaginatedResult<InventoryItemDto>
            {
                Data = _mapper.Map<List<InventoryItemDto>>(filteredItems),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<List<InventoryItemDto>> GetAllInventoryItemsAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            return _mapper.Map<List<InventoryItemDto>>(items.OrderBy(i => i.Name));
        }

        public async Task<InventoryItemDto> GetInventoryItemByIdAsync(int itemId)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item == null)
                throw new Exception("الصنف غير موجود");

            return _mapper.Map<InventoryItemDto>(item);
        }

        public async Task<InventoryItemDto> AddInventoryItemAsync(InventoryItemDto itemDto)
        {
            // التحقق من صحة البيانات
            if (string.IsNullOrWhiteSpace(itemDto.Name))
                throw new Exception("اسم الصنف مطلوب");

            if (string.IsNullOrWhiteSpace(itemDto.Code))
                throw new Exception("رمز الصنف مطلوب");

            if (itemDto.Quantity < 0)
                throw new Exception("الكمية لا يمكن أن تكون سالبة");

            var item = _mapper.Map<InventoryItem>(itemDto);
            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = DateTime.Now;
            item.Status = item.Quantity > 0 ? InventoryStatus.InStock : InventoryStatus.OutOfStock;

            await _itemRepository.AddAsync(item);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<InventoryItemDto>(item);
        }

        public async Task UpdateInventoryItemAsync(int itemId, InventoryItemDto itemDto)
        {
            var existingItem = await _itemRepository.GetByIdAsync(itemId);
            if (existingItem == null)
                throw new Exception("الصنف غير موجود");

            _mapper.Map(itemDto, existingItem);
            existingItem.UpdatedAt = DateTime.Now;
            existingItem.Status = existingItem.Quantity > 0 ? InventoryStatus.InStock : InventoryStatus.OutOfStock;

            _itemRepository.Update(existingItem);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteInventoryItemAsync(int itemId)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item == null)
                throw new Exception("الصنف غير موجود");

            _itemRepository.Delete(item);
            await _unitOfWork.CommitAsync();
        }

        public async Task<InventoryStatsDto> GetInventoryStatsAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            var movements = await _movementRepository.GetAllAsync();

            var totalItems = items.Count();
            var inStockItems = items.Count(i => i.Status == InventoryStatus.InStock);
            var lowStockItems = items.Count(i => i.Quantity <= i.ReorderLevel && i.Quantity > 0);
            var outOfStockItems = items.Count(i => i.Status == InventoryStatus.OutOfStock);
            var totalValue = items.Sum(i => i.Quantity * i.UnitPrice);

            var recentMovements = movements.Where(m => m.MovementDate >= DateTime.Now.AddDays(-30)).Count();

            return new InventoryStatsDto
            {
                TotalItems = totalItems,
                InStockItems = inStockItems,
                LowStockItems = lowStockItems,
                OutOfStockItems = outOfStockItems,
                TotalValue = totalValue,
                RecentMovements = recentMovements,
                CategoriesCount = (await _categoryRepository.GetAllAsync()).Count()
            };
        }

        public async Task<List<InventoryItemDto>> GetLowStockItemsAsync(int threshold)
        {
            var items = await _itemRepository.GetAllAsync();
            var lowStockItems = items.Where(i => i.Quantity <= threshold && i.Quantity > 0)
                                   .OrderBy(i => i.Quantity)
                                   .ToList();

            return _mapper.Map<List<InventoryItemDto>>(lowStockItems);
        }

        public async Task<List<InventoryItemDto>> GetOutOfStockItemsAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            var outOfStockItems = items.Where(i => i.Status == InventoryStatus.OutOfStock)
                                     .OrderBy(i => i.Name)
                                     .ToList();

            return _mapper.Map<List<InventoryItemDto>>(outOfStockItems);
        }

        public async Task<List<InventoryMovementDto>> GetItemMovementsAsync(int itemId)
        {
            var movements = await _movementRepository.GetAllAsync();
            var itemMovements = movements.Where(m => m.InventoryItemId == itemId)
                                       .OrderByDescending(m => m.MovementDate)
                                       .ToList();

            return _mapper.Map<List<InventoryMovementDto>>(itemMovements);
        }

        public async Task<InventoryMovementDto> AddItemMovementAsync(InventoryMovementDto movementDto)
        {
            // التحقق من صحة البيانات
            if (movementDto.Quantity <= 0)
                throw new Exception("الكمية يجب أن تكون أكبر من صفر");

            var movement = _mapper.Map<InventoryMovement>(movementDto);
            movement.MovementDate = DateTime.Now;
            movement.CreatedAt = DateTime.Now;

            await _movementRepository.AddAsync(movement);
            await _unitOfWork.CommitAsync();

            // تحديث كمية الصنف
            var item = await _itemRepository.GetByIdAsync(movementDto.InventoryItemId);
            if (item != null)
            {
                if (movementDto.MovementType == MovementType.In)
                {
                    item.Quantity += movementDto.Quantity;
                }
                else
                {
                    item.Quantity -= movementDto.Quantity;
                }
                item.Status = item.Quantity > 0 ? InventoryStatus.InStock : InventoryStatus.OutOfStock;
                item.UpdatedAt = DateTime.Now;

                _itemRepository.Update(item);
                await _unitOfWork.CommitAsync();
            }

            return _mapper.Map<InventoryMovementDto>(movement);
        }

        public async Task<byte[]> ExportInventoryToExcelAsync()
        {
            var items = await GetAllInventoryItemsAsync();
            // تنفيذ التصدير إلى Excel
            return new byte[0];
        }

        public async Task<byte[]> ExportInventoryToPdfAsync()
        {
            var stats = await GetInventoryStatsAsync();
            var items = await GetAllInventoryItemsAsync();
            // تنفيذ التصدير إلى PDF
            return new byte[0];
        }

        public async Task<List<InventoryCategoryDto>> GetInventoryCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<InventoryCategoryDto>>(categories.OrderBy(c => c.Name));
        }

        public async Task<InventoryItemDto> UpdateItemQuantityAsync(int itemId, int quantity, string reason)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item == null)
                throw new Exception("الصنف غير موجود");

            var oldQuantity = item.Quantity;
            item.Quantity = quantity;
            item.Status = quantity > 0 ? InventoryStatus.InStock : InventoryStatus.OutOfStock;
            item.UpdatedAt = DateTime.Now;

            _itemRepository.Update(item);
            await _unitOfWork.CommitAsync();

            // تسجيل حركة المخزون
            var movement = new InventoryMovement
            {
                InventoryItemId = itemId,
                MovementType = quantity > oldQuantity ? MovementType.In : MovementType.Out,
                Quantity = Math.Abs(quantity - oldQuantity),
                Reason = reason,
                MovementDate = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            await _movementRepository.AddAsync(movement);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<InventoryItemDto>(item);
        }

        public async Task<List<InventoryItemDto>> GetItemsByCategoryAsync(string category)
        {
            var items = await _itemRepository.GetAllAsync();
            var categoryItems = items.Where(i => i.Category == category)
                                   .OrderBy(i => i.Name)
                                   .ToList();

            return _mapper.Map<List<InventoryItemDto>>(categoryItems);
        }

        public async Task<decimal> GetTotalInventoryValueAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            return items.Sum(i => i.Quantity * i.UnitPrice);
        }
    }
}
