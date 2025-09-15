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

    public class CashFundService : Injectable, ICashFundService
    {
        private readonly IRepository<CashTransaction> _transactionRepository;
        private readonly IRepository<CashFund> _fundRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CashFundService(
            IRepository<CashTransaction> transactionRepository,
            IRepository<CashFund> fundRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _fundRepository = fundRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CashFundDto> GetCashFundStatusAsync()
        {
            var fund = await _fundRepository.GetAllAsync();
            var transactions = await _transactionRepository.GetAllAsync();

            var currentBalance = transactions.Where(t => t.Status == TransactionStatus.Approved)
                                           .Sum(t => t.Type == TransactionType.Deposit ? t.Amount : -t.Amount);

            var pendingTransactions = transactions.Count(t => t.Status == TransactionStatus.Pending);
            var todayTransactions = transactions.Count(t => t.TransactionDate.Date == DateTime.Today &&
                                                          t.Status == TransactionStatus.Approved);

            return new CashFundDto
            {
                CurrentBalance = currentBalance,
                PendingTransactionsCount = pendingTransactions,
                TodayTransactionsCount = todayTransactions,
                LastUpdated = DateTime.Now,
                Status = currentBalance >= 0 ? FundStatus.Active : FundStatus.Overdrawn
            };
        }

        public async Task<List<CashTransactionDto>> GetCashTransactionsAsync(DateTime fromDate, DateTime toDate)
        {
            var transactions = await _transactionRepository.GetAllAsync();
            var filteredTransactions = transactions.Where(t => t.TransactionDate >= fromDate &&
                                                             t.TransactionDate <= toDate)
                                                 .OrderByDescending(t => t.TransactionDate)
                                                 .ThenByDescending(t => t.CreatedAt)
                                                 .ToList();

            return _mapper.Map<List<CashTransactionDto>>(filteredTransactions);
        }

        public async Task<List<CashTransactionDto>> GetCashTransactionsByTypeAsync(TransactionType type, DateTime fromDate, DateTime toDate)
        {
            var transactions = await _transactionRepository.GetAllAsync();
            var filteredTransactions = transactions.Where(t => t.Type == type &&
                                                             t.TransactionDate >= fromDate &&
                                                             t.TransactionDate <= toDate &&
                                                             t.Status == TransactionStatus.Approved)
                                                 .OrderByDescending(t => t.TransactionDate)
                                                 .ToList();

            return _mapper.Map<List<CashTransactionDto>>(filteredTransactions);
        }

        public async Task<CashTransactionDto> AddCashTransactionAsync(CashTransactionDto transactionDto)
        {
            // التحقق من صحة البيانات
            if (transactionDto.Amount <= 0)
                throw new Exception("يجب أن يكون المبلغ أكبر من صفر");

            if (string.IsNullOrWhiteSpace(transactionDto.Description))
                throw new Exception("الوصف مطلوب");

            var transaction = _mapper.Map<CashTransaction>(transactionDto);
            transaction.CreatedAt = DateTime.Now;
            transaction.UpdatedAt = DateTime.Now;
            transaction.TransactionNumber = GenerateTransactionNumber();

            // إذا كان نوع الصندوق غير محدد، نستخدم الافتراضي
            if (transaction.CashFundId == 0)
            {
                var defaultFund = await _fundRepository.GetAllAsync();
                if (defaultFund.Any())
                {
                    transaction.CashFundId = defaultFund.First().Id;
                }
            }

            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CashTransactionDto>(transaction);
        }

        public async Task UpdateCashTransactionAsync(int transactionId, CashTransactionDto transactionDto)
        {
            var existingTransaction = await _transactionRepository.GetByIdAsync(transactionId);
            if (existingTransaction == null)
                throw new Exception("المعاملة غير موجودة");

            _mapper.Map(transactionDto, existingTransaction);
            existingTransaction.UpdatedAt = DateTime.Now;

            _transactionRepository.Update(existingTransaction);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCashTransactionAsync(int transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);
            if (transaction == null)
                throw new Exception("المعاملة غير موجودة");

            _transactionRepository.Delete(transaction);
            await _unitOfWork.CommitAsync();
        }

        public async Task<decimal> GetCurrentCashBalanceAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions.Where(t => t.Status == TransactionStatus.Approved)
                             .Sum(t => t.Type == TransactionType.Deposit ? t.Amount : -t.Amount);
        }

        public async Task<decimal> GetCashBalanceAsOfDateAsync(DateTime asOfDate)
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions.Where(t => t.TransactionDate <= asOfDate &&
                                         t.Status == TransactionStatus.Approved)
                             .Sum(t => t.Type == TransactionType.Deposit ? t.Amount : -t.Amount);
        }

        public async Task<CashFundSummaryDto> GetCashFundSummaryAsync(DateTime fromDate, DateTime toDate)
        {
            var transactions = await _transactionRepository.GetAllAsync();
            var filteredTransactions = transactions.Where(t => t.TransactionDate >= fromDate &&
                                                             t.TransactionDate <= toDate &&
                                                             t.Status == TransactionStatus.Approved)
                                                 .ToList();

            var deposits = filteredTransactions.Where(t => t.Type == TransactionType.Deposit).Sum(t => t.Amount);
            var withdrawals = filteredTransactions.Where(t => t.Type == TransactionType.Withdrawal).Sum(t => t.Amount);
            var netChange = deposits - withdrawals;

            return new CashFundSummaryDto
            {
                FromDate = fromDate,
                ToDate = toDate,
                TotalDeposits = deposits,
                TotalWithdrawals = withdrawals,
                NetChange = netChange,
                TransactionCount = filteredTransactions.Count,
                AverageTransactionAmount = filteredTransactions.Any() ? filteredTransactions.Average(t => t.Amount) : 0
            };
        }

        public async Task<byte[]> ExportCashTransactionsToExcelAsync(DateTime fromDate, DateTime toDate)
        {
            var transactions = await GetCashTransactionsAsync(fromDate, toDate);
            // تنفيذ التصدير إلى Excel
            return new byte[0];
        }

        public async Task<byte[]> ExportCashFundReportToPdfAsync(DateTime fromDate, DateTime toDate)
        {
            var summary = await GetCashFundSummaryAsync(fromDate, toDate);
            var transactions = await GetCashTransactionsAsync(fromDate, toDate);
            // تنفيذ التصدير إلى PDF
            return new byte[0];
        }

        public async Task<List<CashTransactionDto>> GetPendingTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            var pendingTransactions = transactions.Where(t => t.Status == TransactionStatus.Pending)
                                                .OrderByDescending(t => t.CreatedAt)
                                                .ToList();

            return _mapper.Map<List<CashTransactionDto>>(pendingTransactions);
        }

        public async Task ApproveTransactionAsync(int transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);
            if (transaction == null)
                throw new Exception("المعاملة غير موجودة");

            transaction.Status = TransactionStatus.Approved;
            transaction.ApprovedAt = DateTime.Now;
            transaction.UpdatedAt = DateTime.Now;

            _transactionRepository.Update(transaction);
            await _unitOfWork.CommitAsync();
        }

        public async Task<decimal> GetDailyCashFlowAsync(DateTime date)
        {
            var transactions = await _transactionRepository.GetAllAsync();
            var dailyTransactions = transactions.Where(t => t.TransactionDate.Date == date.Date &&
                                                          t.Status == TransactionStatus.Approved)
                                              .ToList();

            var deposits = dailyTransactions.Where(t => t.Type == TransactionType.Deposit).Sum(t => t.Amount);
            var withdrawals = dailyTransactions.Where(t => t.Type == TransactionType.Withdrawal).Sum(t => t.Amount);

            return deposits - withdrawals;
        }

        private string GenerateTransactionNumber()
        {
            return $"TRX-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
    }
}
