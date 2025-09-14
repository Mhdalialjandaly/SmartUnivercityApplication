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
    public class AccountingEntryService : Injectable, IAccountingEntryService
    {
        private readonly IRepository<AccountingEntry> _entryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountingEntryService(
            IRepository<AccountingEntry> entryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<AccountingEntryDto>> GetEntriesAsync(int pageNumber, int pageSize, string searchTerm = "", EntryType? type = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = await _entryRepository.GetAllAsync();

            // تطبيق الفلاتر
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(e => e.EntryNumber.Contains(searchTerm) ||
                                       e.Description.Contains(searchTerm));
            }

            if (type.HasValue)
            {
                query = query.Where(e => e.Type == type.Value);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(e => e.EntryDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(e => e.EntryDate <= toDate.Value);
            }

            var entries = query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();

            var totalCount = query.Count();

            return new PaginatedResult<AccountingEntryDto>
            {
                Data = _mapper.Map<List<AccountingEntryDto>>(entries),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<List<AccountingEntryDto>> GetAllEntriesAsync()
        {
            var entries = await _entryRepository.GetAllAsync();
            return _mapper.Map<List<AccountingEntryDto>>(entries.OrderBy(e => e.EntryDate));
        }

        public async Task<AccountingEntryDto> GetEntryByIdAsync(int id)
        {
            var entry = await _entryRepository.GetByIdAsync(id);
            if (entry == null)
                throw new Exception("القيد المحاسبي غير موجود");

            return _mapper.Map<AccountingEntryDto>(entry);
        }

        public async Task<AccountingEntryDto> CreateEntryAsync(AccountingEntryDto entryDto)
        {
            var entry = _mapper.Map<AccountingEntry>(entryDto);

            // التحقق من صحة البيانات
            if (entry.Amount <= 0)
                throw new Exception("يجب أن يكون المبلغ أكبر من صفر");

            if (string.IsNullOrWhiteSpace(entry.EntryNumber))
                throw new Exception("رقم القيد مطلوب");

            entry.CreatedAt = DateTime.Now;
            entry.UpdatedAt = DateTime.Now;

            await _entryRepository.AddAsync(entry);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<AccountingEntryDto>(entry);
        }

        public async Task UpdateEntryAsync(int id, AccountingEntryDto entryDto)
        {
            var existingEntry = await _entryRepository.GetByIdAsync(id);
            if (existingEntry == null)
                throw new Exception("القيد المحاسبي غير موجود");

            // التحقق من صحة البيانات
            if (entryDto.Amount <= 0)
                throw new Exception("يجب أن يكون المبلغ أكبر من صفر");

            if (string.IsNullOrWhiteSpace(entryDto.EntryNumber))
                throw new Exception("رقم القيد مطلوب");

            _mapper.Map(entryDto, existingEntry);
            existingEntry.UpdatedAt = DateTime.Now;

            _entryRepository.Update(existingEntry);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteEntryAsync(int id)
        {
            var entry = await _entryRepository.GetByIdAsync(id);
            if (entry == null)
                throw new Exception("القيد المحاسبي غير موجود");

            _entryRepository.Delete(entry);
            await _unitOfWork.CommitAsync();
        }

        public async Task<AccountingStatsDto> GetAccountingStatsAsync()
        {
            var entries = await _entryRepository.GetAllAsync();

            var debitEntries = entries.Where(e => e.Type == EntryType.Debit);
            var creditEntries = entries.Where(e => e.Type == EntryType.Credit);

            return new AccountingStatsDto
            {
                TotalEntries = entries.Count(),
                TotalDebit = debitEntries.Sum(e => e.Amount),
                TotalCredit = creditEntries.Sum(e => e.Amount),
                NetBalance = debitEntries.Sum(e => e.Amount) - creditEntries.Sum(e => e.Amount),
                RecentEntriesCount = entries.Count(e => e.CreatedAt >= DateTime.Now.AddDays(-30))
            };
        }

        public async Task<decimal> GetTotalDebitAsync()
        {
            var entries = await _entryRepository.GetAllAsync();
            return entries.Where(e => e.Type == EntryType.Debit).Sum(e => e.Amount);
        }

        public async Task<decimal> GetTotalCreditAsync()
        {
            var entries = await _entryRepository.GetAllAsync();
            return entries.Where(e => e.Type == EntryType.Credit).Sum(e => e.Amount);
        }

        public async Task<List<AccountingEntryDto>> GetEntriesByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            var entries = await _entryRepository.GetAllAsync();
            var filteredEntries = entries.Where(e => e.EntryDate >= fromDate && e.EntryDate <= toDate)
                                        .OrderBy(e => e.EntryDate)
                                        .ToList();

            return _mapper.Map<List<AccountingEntryDto>>(filteredEntries);
        }
    }
}
