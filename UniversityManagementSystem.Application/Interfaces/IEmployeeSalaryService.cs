using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IEmployeeSalaryService
    {
        Task<List<EmployeeSalaryDto>> GetAllSalariesAsync();
        Task<List<EmployeeSalaryDto>> GetSalariesByEmployeeAsync(int employeeId);
        Task<EmployeeSalaryDto?> GetSalaryByIdAsync(int id);
        Task<EmployeeSalaryDto> CreateSalaryAsync(EmployeeSalaryDto salaryDto);
        Task UpdateSalaryAsync(int id, EmployeeSalaryDto salaryDto);
        Task DeleteSalaryAsync(int id);
        Task<SalarySummaryDto> GetSalarySummaryAsync();
        Task<List<EmployeeSalaryDto>> GetPendingSalariesAsync();
        Task<decimal> GetEmployeeTotalSalariesAsync(int employeeId);
        Task<decimal> CalculateNetSalaryAsync(decimal baseSalary, decimal allowances, decimal deductions, decimal bonus);
    }
}
