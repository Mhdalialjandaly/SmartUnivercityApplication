using AutoMapper;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class EmployeeSalaryService : IEmployeeSalaryService
    {
        private readonly IRepository<EmployeeSalary> _salaryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeSalaryService(
            IRepository<EmployeeSalary> salaryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _salaryRepository = salaryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<EmployeeSalaryDto>> GetAllSalariesAsync()
        {
            var salaries = await _salaryRepository.GetAllAsync();
            return _mapper.Map<List<EmployeeSalaryDto>>(salaries);
        }

        public async Task<List<EmployeeSalaryDto>> GetSalariesByEmployeeAsync(int employeeId)
        {
            var salaries = await _salaryRepository.GetAllAsync(s => s.EmployeeId == employeeId);
            return _mapper.Map<List<EmployeeSalaryDto>>(salaries);
        }

        public async Task<EmployeeSalaryDto?> GetSalaryByIdAsync(int id)
        {
            var salary = await _salaryRepository.GetByIdAsync(id);
            return salary != null ? _mapper.Map<EmployeeSalaryDto>(salary) : null;
        }

        public async Task<EmployeeSalaryDto> CreateSalaryAsync(EmployeeSalaryDto salaryDto)
        {
            var salary = _mapper.Map<EmployeeSalary>(salaryDto);
            salary.NetSalary = await CalculateNetSalaryAsync(
                salary.BaseSalary,
                salary.Allowances,
                salary.Deductions,
                salary.Bonus);
            salary.SalaryDate = DateTime.Now;

            await _salaryRepository.AddAsync(salary);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<EmployeeSalaryDto>(salary);
        }

        public async Task UpdateSalaryAsync(int id, EmployeeSalaryDto salaryDto)
        {
            var existingSalary = await _salaryRepository.GetByIdAsync(id);
            if (existingSalary == null)
                throw new Exception($"Salary with ID {id} not found");

            _mapper.Map(salaryDto, existingSalary);
            existingSalary.NetSalary = await CalculateNetSalaryAsync(
                existingSalary.BaseSalary,
                existingSalary.Allowances,
                existingSalary.Deductions,
                existingSalary.Bonus);

            _salaryRepository.Update(existingSalary);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteSalaryAsync(int id)
        {
            var salary = await _salaryRepository.GetByIdAsync(id);
            if (salary == null)
                throw new Exception($"Salary with ID {id} not found");

            _salaryRepository.Delete(salary);
            await _unitOfWork.CommitAsync();
        }

        public async Task<SalarySummaryDto> GetSalarySummaryAsync()
        {
            var salaries = await _salaryRepository.GetAllAsync();

            return new SalarySummaryDto
            {
                TotalSalaries = salaries.Count(),
                TotalAmount = salaries.Sum(s => s.NetSalary),
                PendingSalaries = salaries.Count(s => s.Status == "معلق"),
                PendingAmount = salaries.Where(s => s.Status == "معلق").Sum(s => s.NetSalary)
            };
        }

        public async Task<List<EmployeeSalaryDto>> GetPendingSalariesAsync()
        {
            var salaries = await _salaryRepository.GetAllAsync(s => s.Status == "معلق");
            return _mapper.Map<List<EmployeeSalaryDto>>(salaries);
        }

        public async Task<decimal> GetEmployeeTotalSalariesAsync(int employeeId)
        {
            var salaries = await _salaryRepository.GetAllAsync(s => s.EmployeeId == employeeId && s.Status == "مدفوع");
            return salaries.Sum(s => s.NetSalary);
        }

        public async Task<decimal> CalculateNetSalaryAsync(decimal baseSalary, decimal allowances, decimal deductions, decimal bonus)
        {
            return baseSalary + allowances + bonus - deductions;
        }
    }
}
