using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IArchiveService
    {
        Task<PaginatedResult<ArchiveItemDto>> GetArchiveItemsAsync(int pageNumber, int pageSize, string searchTerm = "", int? departmentId = null, ArchiveType type = ArchiveType.All);
        Task<List<ArchiveItemDto>> GettAllArchiveItems();
        Task<ArchiveItemDto> GetArchiveItemByIdAsync(int id);
        Task<ArchiveItemDto> AddArchiveItemAsync(ArchiveItemDto archiveItem);
        Task UpdateArchiveItemAsync(int id, ArchiveItemDto archiveItem);
        Task DeleteArchiveItemAsync(int id);
        Task<ArchiveStatsDto> GetArchiveStatsAsync();
        Task<byte[]> DownloadFileAsync(int itemId);
    }


}
