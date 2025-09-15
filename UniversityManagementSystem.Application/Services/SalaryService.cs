using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Enums;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class SalaryService : Injectable, ISalaryService
    {
        private readonly IRepository<EmployeeSalary> _salaryRepository;
        private readonly IRepository<SalaryPayment> _paymentRepository;
        private readonly IRepository<SalaryDeduction> _deductionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SalaryService(
            IRepository<EmployeeSalary> salaryRepository,
            IRepository<SalaryPayment> paymentRepository,
            IRepository<SalaryDeduction> deductionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _salaryRepository = salaryRepository;
            _paymentRepository = paymentRepository;
            _deductionRepository = deductionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<EmployeeSalaryDto>> GetEmployeeSalariesAsync(int pageNumber, int pageSize, string searchTerm = "", string department = "", SalaryStatus? status = null)
        {
            var salaries = await _salaryRepository.GetAllAsync();
            var query = salaries.AsQueryable();

            // تطبيق الفلاتر
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(s => s.EmployeeName.Contains(searchTerm) ||
                                      s.EmployeeId.Contains(searchTerm) ||
                                      s.Department.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(department))
            {
                query = query.Where(s => s.Department == department);
            }

            if (status.HasValue)
            {
                query = query.Where(s => s.Status == status.Value);
            }

            var filteredSalaries = query.Skip((pageNumber - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToList();

            var totalCount = query.Count();

            return new PaginatedResult<EmployeeSalaryDto>
            {
                Data = _mapper.Map<List<EmployeeSalaryDto>>(filteredSalaries),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<List<EmployeeSalaryDto>> GetAllEmployeeSalariesAsync()
        {
            var salaries = await _salaryRepository.GetAllAsync();
            return _mapper.Map<List<EmployeeSalaryDto>>(salaries.OrderBy(s => s.EmployeeName));
        }

        public async Task<EmployeeSalaryDto> GetEmployeeSalaryByIdAsync(int salaryId)
        {
            var salary = await _salaryRepository.GetByIdAsync(salaryId);
            if (salary == null)
                throw new Exception("سجل الراتب غير موجود");

            return _mapper.Map<EmployeeSalaryDto>(salary);
        }

        public async Task<EmployeeSalaryDto> AddEmployeeSalaryAsync(EmployeeSalaryDto salaryDto)
        {
            // التحقق من صحة البيانات
            if (string.IsNullOrWhiteSpace(salaryDto.EmployeeName))
                throw new Exception("اسم الموظف مطلوب");

            if (string.IsNullOrWhiteSpace(salaryDto.EmployeeId))
                throw new Exception("رقم الموظف مطلوب");

            if (salaryDto.BaseSalary <= 0)
                throw new Exception("الراتب الأساسي يجب أن يكون أكبر من صفر");

            var salary = _mapper.Map<EmployeeSalary>(salaryDto);
            salary.CreatedAt = DateTime.Now;
            salary.UpdatedAt = DateTime.Now;
            salary.NetSalary = CalculateNetSalary(salary);

            await _salaryRepository.AddAsync(salary);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<EmployeeSalaryDto>(salary);
        }

        public async Task UpdateEmployeeSalaryAsync(int salaryId, EmployeeSalaryDto salaryDto)
        {
            var existingSalary = await _salaryRepository.GetByIdAsync(salaryId);
            if (existingSalary == null)
                throw new Exception("سجل الراتب غير موجود");

            _mapper.Map(salaryDto, existingSalary);
            existingSalary.UpdatedAt = DateTime.Now;
            existingSalary.NetSalary = CalculateNetSalary(existingSalary);

            _salaryRepository.Update(existingSalary);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteEmployeeSalaryAsync(int salaryId)
        {
            var salary = await _salaryRepository.GetByIdAsync(salaryId);
            if (salary == null)
                throw new Exception("سجل الراتب غير موجود");

            _salaryRepository.Delete(salary);
            await _unitOfWork.CommitAsync();
        }

        public async Task<SalaryStatsDto> GetSalaryStatsAsync()
        {
            var salaries = await _salaryRepository.GetAllAsync();
            var payments = await _paymentRepository.GetAllAsync();

            var totalEmployees = salaries.Count();
            var activeSalaries = salaries.Count(s => s.Status == SalaryStatus.Active);
            var pendingSalaries = salaries.Count(s => s.Status == SalaryStatus.Pending);

            var totalMonthlySalary = salaries.Where(s => s.Status == SalaryStatus.Active).Sum(s => s.NetSalary);
            var totalPaidThisMonth = payments.Where(p => p.PaymentDate.Month == DateTime.Now.Month &&
                                                      p.PaymentDate.Year == DateTime.Now.Year)
                                           .Sum(p => p.Amount);

            var totalDeductions = salaries.Where(s => s.Status == SalaryStatus.Active).Sum(s => s.TotalDeductions);
            var averageSalary = totalEmployees > 0 ? totalMonthlySalary / totalEmployees : 0;

            var departments = salaries.Select(s => s.Department).Distinct().Count();

            return new SalaryStatsDto
            {
                TotalEmployees = totalEmployees,
                ActiveSalaries = activeSalaries,
                PendingSalaries = pendingSalaries,
                TotalMonthlySalary = totalMonthlySalary,
                TotalPaidThisMonth = totalPaidThisMonth,
                TotalDeductions = totalDeductions,
                AverageSalary = averageSalary,
                DepartmentsCount = departments
            };
        }

        public async Task<List<SalaryPaymentDto>> GetSalaryPaymentsAsync(int employeeId)
        {
            var payments = await _paymentRepository.GetAllAsync();
            var employeePayments = payments.Where(p => p.EmployeeSalaryId == employeeId)
                                         .OrderByDescending(p => p.PaymentDate)
                                         .ToList();

            return _mapper.Map<List<SalaryPaymentDto>>(employeePayments);
        } 
        
        public async Task<List<SalaryPaymentDto>> GetAllSalaryPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return _mapper.Map<List<SalaryPaymentDto>>(payments.ToList());
        }

        public async Task<SalaryPaymentDto> AddSalaryPaymentAsync(SalaryPaymentDto paymentDto)
        {
            var payment = _mapper.Map<SalaryPayment>(paymentDto);
            payment.PaymentDate = DateTime.Now;
            payment.CreatedAt = DateTime.Now;

            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<SalaryPaymentDto>(payment);
        }

        public async Task<byte[]> ExportSalariesToExcelAsync(DateTime fromDate, DateTime toDate)
        {
            var salaries = await GetAllEmployeeSalariesAsync();
            // تنفيذ التصدير إلى Excel
            return new byte[0];
        }

        public async Task<byte[]> ExportSalariesToPdfAsync(DateTime fromDate, DateTime toDate)
        {
            var stats = await GetSalaryStatsAsync();
            var salaries = await GetAllEmployeeSalariesAsync();
            // تنفيذ التصدير إلى PDF
            return new byte[0];
        }

        public async Task<List<EmployeeSalaryDto>> GetEmployeesByDepartmentAsync(string department)
        {
            var salaries = await _salaryRepository.GetAllAsync();
            var departmentSalaries = salaries.Where(s => s.Department == department)
                                           .OrderBy(s => s.EmployeeName)
                                           .ToList();

            return _mapper.Map<List<EmployeeSalaryDto>>(departmentSalaries);
        }

        public async Task<decimal> GetTotalMonthlySalaryAsync()
        {
            var salaries = await _salaryRepository.GetAllAsync();
            return salaries.Where(s => s.Status == SalaryStatus.Active).Sum(s => s.NetSalary);
        }

        public async Task<decimal> GetTotalPaidSalariesAsync(DateTime fromDate, DateTime toDate)
        {
            var payments = await _paymentRepository.GetAllAsync();
            return payments.Where(p => p.PaymentDate >= fromDate && p.PaymentDate <= toDate)
                         .Sum(p => p.Amount);
        }

        public async Task<List<EmployeeSalaryDto>> GetPendingSalariesAsync()
        {
            var salaries = await _salaryRepository.GetAllAsync();
            var pendingSalaries = salaries.Where(s => s.Status == SalaryStatus.Pending)
                                        .OrderBy(s => s.EmployeeName)
                                        .ToList();

            return _mapper.Map<List<EmployeeSalaryDto>>(pendingSalaries);
        }

        public async Task ProcessMonthlySalariesAsync(DateTime payrollDate)
        {
            var activeSalaries = await _salaryRepository.GetAllAsync();
            var activeEmployees = activeSalaries.Where(s => s.Status == SalaryStatus.Active).ToList();

            foreach (var salary in activeEmployees)
            {
                var payment = new SalaryPayment
                {
                    EmployeeSalaryId = salary.Id,
                    Amount = salary.NetSalary,
                    PaymentDate = payrollDate,
                    PaymentMethod = "تحويل بنكي",
                    Status = PaymentStatus.Paid,
                    Notes = "راتب شهري",
                    CreatedAt = DateTime.Now
                };

                await _paymentRepository.AddAsync(payment);
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<SalaryDeductionDto>> GetEmployeeDeductionsAsync(int employeeId)
        {
            var deductions = await _deductionRepository.GetAllAsync();
            var employeeDeductions = deductions.Where(d => d.EmployeeSalaryId == employeeId)
                                             .OrderByDescending(d => d.DeductionDate)
                                             .ToList();

            return _mapper.Map<List<SalaryDeductionDto>>(employeeDeductions);
        }

        public async Task<SalaryDeductionDto> AddSalaryDeductionAsync(SalaryDeductionDto deductionDto)
        {
            var deduction = _mapper.Map<SalaryDeduction>(deductionDto);
            deduction.DeductionDate = DateTime.Now;
            deduction.CreatedAt = DateTime.Now;

            await _deductionRepository.AddAsync(deduction);
            await _unitOfWork.CommitAsync();

            // تحديث إجمالي الخصومات في سجل الراتب
            var salary = await _salaryRepository.GetByIdAsync(deductionDto.EmployeeSalaryId);
            if (salary != null)
            {
                salary.TotalDeductions = (await GetEmployeeDeductionsAsync(deductionDto.EmployeeSalaryId)).Sum(d => d.Amount);
                salary.NetSalary = CalculateNetSalary(salary);
                salary.UpdatedAt = DateTime.Now;

                _salaryRepository.Update(salary);
                await _unitOfWork.CommitAsync();
            }

            return _mapper.Map<SalaryDeductionDto>(deduction);
        }

        private decimal CalculateNetSalary(EmployeeSalary salary)
        {
            return salary.BaseSalary + salary.Allowances - salary.TotalDeductions;
        }
    }
}
