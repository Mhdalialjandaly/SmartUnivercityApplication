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
    public class ArchiveService : Injectable, IArchiveService
    {
        private readonly IRepository<ArchiveItem> _archiveRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveService(
            IRepository<ArchiveItem> archiveRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _archiveRepository = archiveRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<ArchiveItemDto>> GetArchiveItemsAsync(int pageNumber, int pageSize, string searchTerm = "", int? departmentId = null, ArchiveType type = ArchiveType.All)
        {
            var items = await _archiveRepository.GetPagedAsync(pageNumber, pageSize);

            return new PaginatedResult<ArchiveItemDto>
            {
                Data = _mapper.Map<List<ArchiveItemDto>>(items),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = items.Count(),
                TotalPages = (int)Math.Ceiling(items.Count() / (double)pageSize)
            };
        }

        public async Task<ArchiveItemDto> GetArchiveItemByIdAsync(int id)
        {
            var item = await _archiveRepository.GetByIdAsync(id);
            return _mapper.Map<ArchiveItemDto>(item);
        }

        public async Task<ArchiveItemDto> AddArchiveItemAsync(ArchiveItemDto archiveItemDto)
        {
            var item = _mapper.Map<ArchiveItem>(archiveItemDto);
            await _archiveRepository.AddAsync(item);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ArchiveItemDto>(item);
        }

        public async Task UpdateArchiveItemAsync(int id, ArchiveItemDto archiveItemDto)
        {
            var existingItem = await _archiveRepository.GetByIdAsync(id);
            if (existingItem == null)
                throw new Exception("Archive item not found");

            _mapper.Map(archiveItemDto, existingItem);
            _archiveRepository.Update(existingItem);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteArchiveItemAsync(int id)
        {
            var item = await _archiveRepository.GetByIdAsync(id);
            if (item == null)
                throw new Exception("Archive item not found");

            _archiveRepository.Delete(item);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ArchiveStatsDto> GetArchiveStatsAsync()
        {
            var items = await _archiveRepository.GetAllAsync();

            return new ArchiveStatsDto
            {
                TotalDocuments = items.Count(),
                ActiveDocuments = items.Count(i => i.Status == "نشطة" && i.ExpiryDate >= DateTime.Now),
                ExpiredDocuments = items.Count(i => i.ExpiryDate < DateTime.Now),
                TotalStorageUsed = items.Sum(i => i.FileSize) / (1024 * 1024) // بالميغابايت
            };
        }

        public async Task<byte[]> DownloadFileAsync(int itemId)
        {
            var item = await _archiveRepository.GetByIdAsync(itemId);
            if (item == null)
                throw new Exception("Archive item not found");

            // هنا يجب إضافة كود لاسترجاع الملف من نظام التخزين
            return new byte[0]; // يجب استبدالها ببيانات الملف الفعلية
        }
    }
}
