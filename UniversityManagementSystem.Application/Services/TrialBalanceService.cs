using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class TrialBalanceService : Injectable, ITrialBalanceService
    {
        private readonly IRepository<AccountingEntry> _entryRepository;
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TrialBalanceService(
            IRepository<AccountingEntry> entryRepository,
            IRepository<Account> accountRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _entryRepository = entryRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TrialBalanceDto> GenerateTrialBalanceAsync(DateTime fromDate, DateTime toDate)
        {
            var accounts = await _accountRepository.GetAllAsync();
            var entries = await _entryRepository.GetAllAsync();
                
            var filteredEntries = entries.Where(e => e.EntryDate >= fromDate && e.EntryDate <= toDate).ToList();

            var trialBalanceAccounts = new List<TrialBalanceAccountDto>();

            foreach (var account in accounts)
            {
                var accountEntries = filteredEntries.Where(e => e.AccountCode == account.Code).ToList();

                var debitTotal = accountEntries.Where(e => e.Type == Domain.Enums.EntryType.Debit).Sum(e => e.Amount);
                var creditTotal = accountEntries.Where(e => e.Type == Domain.Enums.EntryType.Credit).Sum(e => e.Amount);

                if (debitTotal > 0 || creditTotal > 0)
                {
                    trialBalanceAccounts.Add(new TrialBalanceAccountDto
                    {
                        AccountCode = account.Code,
                        AccountName = account.Name,
                        AccountType = account.AccountType,
                        DebitAmount = debitTotal,
                        CreditAmount = creditTotal,
                        Balance = debitTotal - creditTotal
                    });
                }
            }

            var totalDebit = trialBalanceAccounts.Sum(a => a.DebitAmount);
            var totalCredit = trialBalanceAccounts.Sum(a => a.CreditAmount);
            var difference = Math.Abs(totalDebit - totalCredit);

            return new TrialBalanceDto
            {
                Accounts = trialBalanceAccounts,
                FromDate = fromDate,
                ToDate = toDate,
                GeneratedAt = DateTime.Now,
                TotalDebit = totalDebit,
                TotalCredit = totalCredit,
                IsBalanced = difference < 0.01m,
                Difference = difference
            };
        }

        public async Task<List<TrialBalanceAccountDto>> GetTrialBalanceAccountsAsync(DateTime fromDate, DateTime toDate)
        {
            var trialBalance = await GenerateTrialBalanceAsync(fromDate, toDate);
            return trialBalance.Accounts;
        }

        public async Task<TrialBalanceSummaryDto> GetTrialBalanceSummaryAsync(DateTime fromDate, DateTime toDate)
        {
            var trialBalance = await GenerateTrialBalanceAsync(fromDate, toDate);

            return new TrialBalanceSummaryDto
            {
                TotalAccounts = trialBalance.Accounts.Count,
                TotalDebit = trialBalance.TotalDebit,
                TotalCredit = trialBalance.TotalCredit,
                Difference = trialBalance.Difference,
                IsBalanced = trialBalance.IsBalanced,
                FromDate = fromDate,
                ToDate = toDate,
                GeneratedAt = trialBalance.GeneratedAt
            };
        }

        public async Task<byte[]> ExportTrialBalanceToExcelAsync(DateTime fromDate, DateTime toDate)
        {
            // هنا سيتم تنفيذ تصدير إلى Excel
            // يمكن استخدام مكتبة مثل ClosedXML أو EPPlus
            var trialBalance = await GenerateTrialBalanceAsync(fromDate, toDate);

            // تنفيذ تصدير إلى Excel
            return new byte[0]; // يجب استبدالها بالتنفيذ الفعلي
        }

        public async Task<byte[]> ExportTrialBalanceToPdfAsync(DateTime fromDate, DateTime toDate)
        {
            // هنا سيتم تنفيذ تصدير إلى PDF
            // يمكن استخدام مكتبة مثل iTextSharp أو QuestPDF
            var trialBalance = await GenerateTrialBalanceAsync(fromDate, toDate);

            // تنفيذ تصدير إلى PDF
            return new byte[0]; // يجب استبدالها بالتنفيذ الفعلي
        }

        public async Task<List<TrialBalanceAccountDto>> GetTrialBalanceByAccountTypeAsync(DateTime fromDate, DateTime toDate, string accountType)
        {
            var trialBalance = await GenerateTrialBalanceAsync(fromDate, toDate);
            return trialBalance.Accounts.Where(a => a.AccountType == accountType).ToList();
        }

        public async Task<decimal> GetAccountBalanceAsync(string accountCode, DateTime fromDate, DateTime toDate)
        {
            var entries = await _entryRepository.GetAllAsync();
            var accountEntries = entries.Where(e => e.AccountCode == accountCode &&
                                                  e.EntryDate >= fromDate &&
                                                  e.EntryDate <= toDate).ToList();

            var debitTotal = accountEntries.Where(e => e.Type == Domain.Enums.EntryType.Debit).Sum(e => e.Amount);
            var creditTotal = accountEntries.Where(e => e.Type == Domain.Enums.EntryType.Credit).Sum(e => e.Amount);

            return debitTotal - creditTotal;
        }
    }
}
