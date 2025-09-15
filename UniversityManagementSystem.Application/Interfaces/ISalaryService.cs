using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface ISalaryService
    {
        Task<PaginatedResult<EmployeeSalaryDto>> GetEmployeeSalariesAsync(int pageNumber, int pageSize, string searchTerm = "", string department = "", SalaryStatus? status = null);
        Task<List<EmployeeSalaryDto>> GetAllEmployeeSalariesAsync();
        Task<EmployeeSalaryDto> GetEmployeeSalaryByIdAsync(int salaryId);
        Task<EmployeeSalaryDto> AddEmployeeSalaryAsync(EmployeeSalaryDto salaryDto);
        Task UpdateEmployeeSalaryAsync(int salaryId, EmployeeSalaryDto salaryDto);
        Task DeleteEmployeeSalaryAsync(int salaryId);
        Task<SalaryStatsDto> GetSalaryStatsAsync();

        Task<List<SalaryPaymentDto>> GetSalaryPaymentsAsync(int employeeId);
        Task<List<SalaryPaymentDto>> GetAllSalaryPaymentsAsync();
        Task<SalaryPaymentDto> AddSalaryPaymentAsync(SalaryPaymentDto paymentDto);
        Task<byte[]> ExportSalariesToExcelAsync(DateTime fromDate, DateTime toDate);
        Task<byte[]> ExportSalariesToPdfAsync(DateTime fromDate, DateTime toDate);
        Task<List<EmployeeSalaryDto>> GetEmployeesByDepartmentAsync(string department);
        Task<decimal> GetTotalMonthlySalaryAsync();
        Task<decimal> GetTotalPaidSalariesAsync(DateTime fromDate, DateTime toDate);
        Task<List<EmployeeSalaryDto>> GetPendingSalariesAsync();
        Task ProcessMonthlySalariesAsync(DateTime payrollDate);
        Task<List<SalaryDeductionDto>> GetEmployeeDeductionsAsync(int employeeId);
        Task<SalaryDeductionDto> AddSalaryDeductionAsync(SalaryDeductionDto deductionDto);
    }
}
