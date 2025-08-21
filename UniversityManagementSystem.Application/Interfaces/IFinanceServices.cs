
using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IFinanceServices
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
    
        Task<List<EmployeeSalaryDto>> GetRecentSalariesAsync(int count = 10);

        // توليد التقارير
        Task<byte[]> GenerateFinancialReportAsync(
            string reportType,
            DateTime startDate,
            DateTime endDate,
            int departmentId,
            string transactionType);

        // الإحصائيات الشهرية
        Task<List<MonthlyFinancialData>> GetMonthlyFinancialDataAsync(int year);
        Task<byte[]> GenerateFinancialReportAsync(string type, DateTime startDate, DateTime endDate, int? departmentId, string? transactionType);
        // كيانات مساعدة
        Task<RevenueDataDto> GetRevenueDataAsync(DateTime? startDate = null, DateTime? endDate = null);
        public class ExpenseItem
        {
            public string Description { get; set; }
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }
        }

        public class MonthlyFinancialData
        {
            public int Month { get; set; }
            public string MonthName { get; set; }
            public decimal Revenue { get; set; }
            public decimal Expenses { get; set; }
            public decimal Net { get; set; }
        }
    }
}
