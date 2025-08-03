
using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IFinanceService
    {
        Task<List<FinanceRecordDto>> GetAllFinanceRecordsAsync();
        Task<FinanceRecordDto?> GetFinanceRecordByIdAsync(int id);
        Task<FinanceRecordDto> CreateFinanceRecordAsync(FinanceRecordDto financeDto);
        Task UpdateFinanceRecordAsync(int id, FinanceRecordDto financeDto);
        Task DeleteFinanceRecordAsync(int id);
        Task<decimal> GetTotalIncomeAsync();
        Task<decimal> GetTotalExpensesAsync();
        Task<FinanceSummaryDto> GetFinanceSummaryAsync();
        Task<int> GetTotalTransactionsAsync();
        Task<decimal> GetTotalRevenueAsync();

        Task<List<FinanceRecordDto>> GetEmployeeSalariesAsync();
        Task<List<FinanceRecordDto>> GetBuildingMaintenanceAsync();
        Task<List<FinanceRecordDto>> GetEducationalEquipmentAsync();
        Task<List<FinanceRecordDto>> GetInternetServicesAsync();
        Task<List<FinanceRecordDto>> GetUtilitiesAsync();
        Task<List<StudentPaymentDto>> GetRecentPaymentsAsync(int count);

        Task<byte[]> GenerateFinancialReportAsync(string type, DateTime startDate, DateTime endDate, int? departmentId, string? transactionType);
    }
}
