using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Enums;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class FinancialStatementsService : Injectable, IFinancialStatementsService
    {
        private readonly IRepository<AccountingEntry> _entryRepository;
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FinancialStatementsService(
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

        public async Task<IncomeStatementDto> GenerateIncomeStatementAsync(DateTime fromDate, DateTime toDate)
        {
            var entries = await _entryRepository.GetAllAsync();
            var accounts = await _accountRepository.GetAllAsync();

            var filteredEntries = entries.Where(e => e.EntryDate >= fromDate && e.EntryDate <= toDate).ToList();

            // حساب الإيرادات
            var revenueAccounts = accounts.Where(a => a.AccountType == "إيرادات" || a.AccountType == "Revenue").ToList();
            var revenueItems = new List<FinancialStatementItemDto>();
            decimal totalRevenue = 0;

            foreach (var account in revenueAccounts)
            {
                var accountEntries = filteredEntries.Where(e => e.AccountCode == account.Code).ToList();
                var creditAmount = accountEntries.Where(e => e.Type == EntryType.Credit).Sum(e => e.Amount);
                var debitAmount = accountEntries.Where(e => e.Type == EntryType.Debit).Sum(e => e.Amount);
                var netAmount = creditAmount - debitAmount;

                if (Math.Abs(netAmount) > 0.01m)
                {
                    revenueItems.Add(new FinancialStatementItemDto
                    {
                        AccountCode = account.Code,
                        AccountName = account.Name,
                        Amount = Math.Abs(netAmount)
                    });
                    totalRevenue += Math.Abs(netAmount);
                }
            }

            // حساب المصروفات
            var expenseAccounts = accounts.Where(a => a.AccountType == "مصروفات" || a.AccountType == "Expenses").ToList();
            var expenseItems = new List<FinancialStatementItemDto>();
            decimal totalExpenses = 0;

            foreach (var account in expenseAccounts)
            {
                var accountEntries = filteredEntries.Where(e => e.AccountCode == account.Code).ToList();
                var debitAmount = accountEntries.Where(e => e.Type == EntryType.Debit).Sum(e => e.Amount);
                var creditAmount = accountEntries.Where(e => e.Type == EntryType.Credit).Sum(e => e.Amount);
                var netAmount = debitAmount - creditAmount;

                if (Math.Abs(netAmount) > 0.01m)
                {
                    expenseItems.Add(new FinancialStatementItemDto
                    {
                        AccountCode = account.Code,
                        AccountName = account.Name,
                        Amount = Math.Abs(netAmount)
                    });
                    totalExpenses += Math.Abs(netAmount);
                }
            }

            return new IncomeStatementDto
            {
                FromDate = fromDate,
                ToDate = toDate,
                GeneratedAt = DateTime.Now,
                RevenueItems = revenueItems,
                TotalRevenue = totalRevenue,
                ExpenseItems = expenseItems,
                TotalExpenses = totalExpenses,
                NetIncome = totalRevenue - totalExpenses
            };
        }

        public async Task<BalanceSheetDto> GenerateBalanceSheetAsync(DateTime asOfDate)
        {
            var entries = await _entryRepository.GetAllAsync();
            var accounts = await _accountRepository.GetAllAsync();

            var filteredEntries = entries.Where(e => e.EntryDate <= asOfDate).ToList();

            // حساب الأصول
            var assetAccounts = accounts.Where(a => a.AccountType == "أصول" || a.AccountType == "Assets").ToList();
            var assetItems = new List<FinancialStatementItemDto>();
            decimal totalAssets = 0;

            foreach (var account in assetAccounts)
            {
                var accountEntries = filteredEntries.Where(e => e.AccountCode == account.Code).ToList();
                var debitAmount = accountEntries.Where(e => e.Type == EntryType.Debit).Sum(e => e.Amount);
                var creditAmount = accountEntries.Where(e => e.Type == EntryType.Credit).Sum(e => e.Amount);
                var balance = debitAmount - creditAmount;

                if (Math.Abs(balance) > 0.01m)
                {
                    assetItems.Add(new FinancialStatementItemDto
                    {
                        AccountCode = account.Code,
                        AccountName = account.Name,
                        Amount = Math.Abs(balance)
                    });
                    totalAssets += Math.Abs(balance);
                }
            }

            // حساب الخصوم
            var liabilityAccounts = accounts.Where(a => a.AccountType == "خصوم" || a.AccountType == "Liabilities").ToList();
            var liabilityItems = new List<FinancialStatementItemDto>();
            decimal totalLiabilities = 0;

            foreach (var account in liabilityAccounts)
            {
                var accountEntries = filteredEntries.Where(e => e.AccountCode == account.Code).ToList();
                var creditAmount = accountEntries.Where(e => e.Type == EntryType.Credit).Sum(e => e.Amount);
                var debitAmount = accountEntries.Where(e => e.Type == EntryType.Debit).Sum(e => e.Amount);
                var balance = creditAmount - debitAmount;

                if (Math.Abs(balance) > 0.01m)
                {
                    liabilityItems.Add(new FinancialStatementItemDto
                    {
                        AccountCode = account.Code,
                        AccountName = account.Name,
                        Amount = Math.Abs(balance)
                    });
                    totalLiabilities += Math.Abs(balance);
                }
            }

            // حساب حقوق الملكية
            var equityAccounts = accounts.Where(a => a.AccountType == "حقوق الملكية" || a.AccountType == "Equity").ToList();
            var equityItems = new List<FinancialStatementItemDto>();
            decimal totalEquity = 0;

            foreach (var account in equityAccounts)
            {
                var accountEntries = filteredEntries.Where(e => e.AccountCode == account.Code).ToList();
                var creditAmount = accountEntries.Where(e => e.Type == EntryType.Credit).Sum(e => e.Amount);
                var debitAmount = accountEntries.Where(e => e.Type == EntryType.Debit).Sum(e => e.Amount);
                var balance = creditAmount - debitAmount;

                if (Math.Abs(balance) > 0.01m)
                {
                    equityItems.Add(new FinancialStatementItemDto
                    {
                        AccountCode = account.Code,
                        AccountName = account.Name,
                        Amount = Math.Abs(balance)
                    });
                    totalEquity += Math.Abs(balance);
                }
            }

            return new BalanceSheetDto
            {
                AsOfDate = asOfDate,
                GeneratedAt = DateTime.Now,
                AssetItems = assetItems,
                TotalAssets = totalAssets,
                LiabilityItems = liabilityItems,
                TotalLiabilities = totalLiabilities,
                EquityItems = equityItems,
                TotalEquity = totalEquity,
                TotalLiabilitiesAndEquity = totalLiabilities + totalEquity
            };
        }

        public async Task<CashFlowStatementDto> GenerateCashFlowStatementAsync(DateTime fromDate, DateTime toDate)
        {
            var entries = await _entryRepository.GetAllAsync();
            var accounts = await _accountRepository.GetAllAsync();

            var filteredEntries = entries.Where(e => e.EntryDate >= fromDate && e.EntryDate <= toDate).ToList();

            // حساب التدفقات النقدية من الأنشطة التشغيلية
            var operatingAccounts = accounts.Where(a => a.AccountType == "التشغيلية" || a.AccountType == "Operating").ToList();
            var operatingItems = new List<FinancialStatementItemDto>();
            decimal netOperatingCashFlow = 0;

            foreach (var account in operatingAccounts)
            {
                var accountEntries = filteredEntries.Where(e => e.AccountCode == account.Code).ToList();
                var debitAmount = accountEntries.Where(e => e.Type == EntryType.Debit).Sum(e => e.Amount);
                var creditAmount = accountEntries.Where(e => e.Type == EntryType.Credit).Sum(e => e.Amount);
                var netAmount = debitAmount - creditAmount;

                if (Math.Abs(netAmount) > 0.01m)
                {
                    operatingItems.Add(new FinancialStatementItemDto
                    {
                        AccountCode = account.Code,
                        AccountName = account.Name,
                        Amount = Math.Abs(netAmount)
                    });
                    netOperatingCashFlow += netAmount;
                }
            }

            // حساب التدفقات النقدية من الأنشطة الاستثمارية
            var investingAccounts = accounts.Where(a => a.AccountType == "استثمارية" || a.AccountType == "Investing").ToList();
            var investingItems = new List<FinancialStatementItemDto>();
            decimal netInvestingCashFlow = 0;

            foreach (var account in investingAccounts)
            {
                var accountEntries = filteredEntries.Where(e => e.AccountCode == account.Code).ToList();
                var debitAmount = accountEntries.Where(e => e.Type == EntryType.Debit).Sum(e => e.Amount);
                var creditAmount = accountEntries.Where(e => e.Type == EntryType.Credit).Sum(e => e.Amount);
                var netAmount = debitAmount - creditAmount;

                if (Math.Abs(netAmount) > 0.01m)
                {
                    investingItems.Add(new FinancialStatementItemDto
                    {
                        AccountCode = account.Code,
                        AccountName = account.Name,
                        Amount = Math.Abs(netAmount)
                    });
                    netInvestingCashFlow += netAmount;
                }
            }

            // حساب التدفقات النقدية من الأنشطة التمويلية
            var financingAccounts = accounts.Where(a => a.AccountType == "تمويلية" || a.AccountType == "Financing").ToList();
            var financingItems = new List<FinancialStatementItemDto>();
            decimal netFinancingCashFlow = 0;

            foreach (var account in financingAccounts)
            {
                var accountEntries = filteredEntries.Where(e => e.AccountCode == account.Code).ToList();
                var debitAmount = accountEntries.Where(e => e.Type == EntryType.Debit).Sum(e => e.Amount);
                var creditAmount = accountEntries.Where(e => e.Type == EntryType.Credit).Sum(e => e.Amount);
                var netAmount = debitAmount - creditAmount;

                if (Math.Abs(netAmount) > 0.01m)
                {
                    financingItems.Add(new FinancialStatementItemDto
                    {
                        AccountCode = account.Code,
                        AccountName = account.Name,
                        Amount = Math.Abs(netAmount)
                    });
                    netFinancingCashFlow += netAmount;
                }
            }

            return new CashFlowStatementDto
            {
                FromDate = fromDate,
                ToDate = toDate,
                GeneratedAt = DateTime.Now,
                OperatingItems = operatingItems,
                NetOperatingCashFlow = netOperatingCashFlow,
                InvestingItems = investingItems,
                NetInvestingCashFlow = netInvestingCashFlow,
                FinancingItems = financingItems,
                NetFinancingCashFlow = netFinancingCashFlow,
                NetChangeInCash = netOperatingCashFlow + netInvestingCashFlow + netFinancingCashFlow
            };
        }

        public async Task<EquityStatementDto> GenerateEquityStatementAsync(DateTime fromDate, DateTime toDate)
        {
            var entries = await _entryRepository.GetAllAsync();
            var accounts = await _accountRepository.GetAllAsync();

            var equityAccounts = accounts.Where(a => a.AccountType == "حقوق الملكية" || a.AccountType == "Equity").ToList();
            var equityItems = new List<EquityStatementItemDto>();
            decimal totalEquityChange = 0;

            foreach (var account in equityAccounts)
            {
                var beginningBalance = await GetAccountBalanceAsync(account.Code, DateTime.MinValue, fromDate.AddDays(-1));
                var endingBalance = await GetAccountBalanceAsync(account.Code, DateTime.MinValue, toDate);
                var change = endingBalance - beginningBalance;

                equityItems.Add(new EquityStatementItemDto
                {
                    AccountCode = account.Code,
                    AccountName = account.Name,
                    BeginningBalance = beginningBalance,
                    EndingBalance = endingBalance,
                    Change = change
                });

                totalEquityChange += change;
            }

            return new EquityStatementDto
            {
                FromDate = fromDate,
                ToDate = toDate,
                GeneratedAt = DateTime.Now,
                EquityItems = equityItems,
                TotalEquityChange = totalEquityChange
            };
        }

        public async Task<FinancialStatementsSummaryDto> GenerateFinancialStatementsSummaryAsync(DateTime fromDate, DateTime toDate)
        {
            var incomeStatement = await GenerateIncomeStatementAsync(fromDate, toDate);
            var balanceSheet = await GenerateBalanceSheetAsync(toDate);
            var cashFlow = await GenerateCashFlowStatementAsync(fromDate, toDate);

            return new FinancialStatementsSummaryDto
            {
                FromDate = fromDate,
                ToDate = toDate,
                GeneratedAt = DateTime.Now,
                NetIncome = incomeStatement.NetIncome,
                TotalAssets = balanceSheet.TotalAssets,
                TotalLiabilities = balanceSheet.TotalLiabilities,
                TotalEquity = balanceSheet.TotalEquity,
                NetCashFlow = cashFlow.NetChangeInCash,
                Revenue = incomeStatement.TotalRevenue,
                Expenses = incomeStatement.TotalExpenses
            };
        }

        public async Task<byte[]> ExportIncomeStatementToExcelAsync(DateTime fromDate, DateTime toDate)
        {
            var incomeStatement = await GenerateIncomeStatementAsync(fromDate, toDate);
            // تنفيذ التصدير إلى Excel
            return new byte[0];
        }

        public async Task<byte[]> ExportBalanceSheetToExcelAsync(DateTime asOfDate)
        {
            var balanceSheet = await GenerateBalanceSheetAsync(asOfDate);
            // تنفيذ التصدير إلى Excel
            return new byte[0];
        }

        public async Task<byte[]> ExportFinancialStatementsToPdfAsync(DateTime fromDate, DateTime toDate)
        {
            var summary = await GenerateFinancialStatementsSummaryAsync(fromDate, toDate);
            // تنفيذ التصدير إلى PDF
            return new byte[0];
        }

        public async Task<List<AccountDto>> GetIncomeAccountsAsync(DateTime fromDate, DateTime toDate)
        {
            var accounts = await _accountRepository.GetAllAsync();
            return _mapper.Map<List<AccountDto>>(accounts.Where(a => a.AccountType == "إيرادات" || a.AccountType == "Revenue").ToList());
        }

        public async Task<List<AccountDto>> GetExpenseAccountsAsync(DateTime fromDate, DateTime toDate)
        {
            var accounts = await _accountRepository.GetAllAsync();
            return _mapper.Map<List<AccountDto>>(accounts.Where(a => a.AccountType == "مصروفات" || a.AccountType == "Expenses").ToList());
        }

        public async Task<List<AccountDto>> GetAssetAccountsAsync(DateTime asOfDate)
        {
            var accounts = await _accountRepository.GetAllAsync();
            return _mapper.Map<List<AccountDto>>(accounts.Where(a => a.AccountType == "أصول" || a.AccountType == "Assets").ToList());
        }

        public async Task<List<AccountDto>> GetLiabilityAccountsAsync(DateTime asOfDate)
        {
            var accounts = await _accountRepository.GetAllAsync();
            return _mapper.Map<List<AccountDto>>(accounts.Where(a => a.AccountType == "خصوم" || a.AccountType == "Liabilities").ToList());
        }

        public async Task<List<AccountDto>> GetEquityAccountsAsync(DateTime asOfDate)
        {
            var accounts = await _accountRepository.GetAllAsync();
            return _mapper.Map<List<AccountDto>>(accounts.Where(a => a.AccountType == "حقوق الملكية" || a.AccountType == "Equity").ToList());
        }

        private async Task<decimal> GetAccountBalanceAsync(string accountCode, DateTime fromDate, DateTime toDate)
        {
            var entries = await _entryRepository.GetAllAsync();
            var accountEntries = entries.Where(e => e.AccountCode == accountCode &&
                                                  e.EntryDate >= fromDate &&
                                                  e.EntryDate <= toDate).ToList();

            var debitTotal = accountEntries.Where(e => e.Type == EntryType.Debit).Sum(e => e.Amount);
            var creditTotal = accountEntries.Where(e => e.Type == EntryType.Credit).Sum(e => e.Amount);

            return debitTotal - creditTotal;
        }
    }
}
