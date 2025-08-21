using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IEmployeeSalaryServices
    {
        Task<List<EmployeeSalaryDto>> GetAllSalariesAsync();
        Task<List<EmployeeSalaryDto>> GetSalariesByEmployeeAsync(int employeeId);
        Task<EmployeeSalaryDto> GetSalaryByIdAsync(int id);
        Task<EmployeeSalaryDto> CreateSalaryAsync(EmployeeSalaryDto salaryDto);
        Task UpdateSalaryAsync(int id, EmployeeSalaryDto salaryDto);
        Task DeleteSalaryAsync(int id);
        Task<SalarySummaryDto> GetSalarySummaryAsync();
        Task<List<EmployeeSalaryDto>> GetPendingSalariesAsync();
        Task<decimal> GetEmployeeTotalSalariesAsync(int employeeId);
        Task<List<EmployeeSalaryDto>> GetRecentSalariesAsync(int count);
        Task<decimal> CalculateNetSalaryAsync(decimal baseSalary, decimal allowances, decimal deductions, decimal bonus);
        Task<PaginatedResult<EmployeeSalaryDto>> GetAllSalariesAsync(
          int pageNumber = 1,
          int pageSize = 10,
          string searchTerm = "",
          string status = "");

        Task<List<EmployeeSalaryDto>> GetSalariesByDepartmentAsync(int departmentId);
    }
}
