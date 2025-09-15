using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface ICashFundService
    {
        Task<CashFundDto> GetCashFundStatusAsync();
        Task<List<CashTransactionDto>> GetCashTransactionsAsync(DateTime fromDate, DateTime toDate);
        Task<List<CashTransactionDto>> GetCashTransactionsByTypeAsync(TransactionType type, DateTime fromDate, DateTime toDate);
        Task<CashTransactionDto> AddCashTransactionAsync(CashTransactionDto transactionDto);
        Task UpdateCashTransactionAsync(int transactionId, CashTransactionDto transactionDto);
        Task DeleteCashTransactionAsync(int transactionId);
        Task<decimal> GetCurrentCashBalanceAsync();
        Task<decimal> GetCashBalanceAsOfDateAsync(DateTime asOfDate);
        Task<CashFundSummaryDto> GetCashFundSummaryAsync(DateTime fromDate, DateTime toDate);
        Task<byte[]> ExportCashTransactionsToExcelAsync(DateTime fromDate, DateTime toDate);
        Task<byte[]> ExportCashFundReportToPdfAsync(DateTime fromDate, DateTime toDate);
        Task<List<CashTransactionDto>> GetPendingTransactionsAsync();
        Task ApproveTransactionAsync(int transactionId);
        Task<decimal> GetDailyCashFlowAsync(DateTime date);
    }
}
