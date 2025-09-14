using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IAccountingEntryService
    {
        Task<PaginatedResult<AccountingEntryDto>> GetEntriesAsync(int pageNumber, int pageSize, string searchTerm = "", EntryType? type = null, DateTime? fromDate = null, DateTime? toDate = null);
        Task<List<AccountingEntryDto>> GetAllEntriesAsync();
        Task<AccountingEntryDto> GetEntryByIdAsync(int id);
        Task<AccountingEntryDto> CreateEntryAsync(AccountingEntryDto entryDto);
        Task UpdateEntryAsync(int id, AccountingEntryDto entryDto);
        Task DeleteEntryAsync(int id);
        Task<AccountingStatsDto> GetAccountingStatsAsync();
        Task<decimal> GetTotalDebitAsync();
        Task<decimal> GetTotalCreditAsync();
        Task<List<AccountingEntryDto>> GetEntriesByDateRangeAsync(DateTime fromDate, DateTime toDate);
    }
}
