

using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IFinancialStatementsService
    {
        Task<IncomeStatementDto> GenerateIncomeStatementAsync(DateTime fromDate, DateTime toDate);
        Task<BalanceSheetDto> GenerateBalanceSheetAsync(DateTime asOfDate);
        Task<CashFlowStatementDto> GenerateCashFlowStatementAsync(DateTime fromDate, DateTime toDate);
        Task<EquityStatementDto> GenerateEquityStatementAsync(DateTime fromDate, DateTime toDate);
        Task<FinancialStatementsSummaryDto> GenerateFinancialStatementsSummaryAsync(DateTime fromDate, DateTime toDate);
        Task<byte[]> ExportIncomeStatementToExcelAsync(DateTime fromDate, DateTime toDate);
        Task<byte[]> ExportBalanceSheetToExcelAsync(DateTime asOfDate);
        Task<byte[]> ExportFinancialStatementsToPdfAsync(DateTime fromDate, DateTime toDate);
        Task<List<AccountDto>> GetIncomeAccountsAsync(DateTime fromDate, DateTime toDate);
        Task<List<AccountDto>> GetExpenseAccountsAsync(DateTime fromDate, DateTime toDate);
        Task<List<AccountDto>> GetAssetAccountsAsync(DateTime asOfDate);
        Task<List<AccountDto>> GetLiabilityAccountsAsync(DateTime asOfDate);
        Task<List<AccountDto>> GetEquityAccountsAsync(DateTime asOfDate);
    }
}
