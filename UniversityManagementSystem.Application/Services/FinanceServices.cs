using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class FinanceServices : Injectable, IFinanceServices
    {
        private readonly IRepository<FinanceRecord> _financeRepository;
        private readonly IRepository<StudentPayment> _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FinanceServices(
            IRepository<FinanceRecord> financeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IRepository<StudentPayment> paymentRepository)
        {
            _financeRepository = financeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
        }
        public async Task<decimal> GetTotalRevenueAsync()
        {
            var records = await _financeRepository.GetAllAsync(r => r.Type == "دخل");
            return records.Sum(r => r.Amount);
        }

        public async Task<int> GetTotalTransactionsAsync()
        {
            var allRecords = await _financeRepository.GetAllAsync();
            return allRecords.Count();
        }

        // مصاريف حسب الفئة
        public async Task<List<FinanceRecordDto>> GetEmployeeSalariesAsync()
        {
            var records = await _financeRepository.GetAllAsync(r => r.Category == "رواتب الموظفين");
            return _mapper.Map<List<FinanceRecordDto>>(records);
        }

        public async Task<List<FinanceRecordDto>> GetBuildingMaintenanceAsync()
        {
            var records = await _financeRepository.GetAllAsync(r => r.Category == "صيانة المباني");
            return _mapper.Map<List<FinanceRecordDto>>(records);
        }

        public async Task<List<FinanceRecordDto>> GetEducationalEquipmentAsync()
        {
            var records = await _financeRepository.GetAllAsync(r => r.Category == "معدات تعليمية");
            return _mapper.Map<List<FinanceRecordDto>>(records);
        }

        public async Task<List<FinanceRecordDto>> GetInternetServicesAsync()
        {
            var records = await _financeRepository.GetAllAsync(r => r.Category == "خدمات الإنترنت");
            return _mapper.Map<List<FinanceRecordDto>>(records);
        }

        public async Task<List<FinanceRecordDto>> GetUtilitiesAsync()
        {
            var records = await _financeRepository.GetAllAsync(r => r.Category == "خدمات المرافق");
            return _mapper.Map<List<FinanceRecordDto>>(records);
        }

        // إنشاء تقرير مالي وهمي (PDF byte[])
        public async Task<byte[]> GenerateFinancialReportAsync(string type, DateTime startDate, DateTime endDate, int? departmentId, string? transactionType)
        {
            // مبدأياً هذه دالة وهمية. يمكنك استبدالها بمولد PDF حقيقي مثل iTextSharp أو QuestPDF
            var filtered = await _financeRepository.GetAllAsync(r =>
                r.Date >= startDate &&
                r.Date <= endDate &&
                (string.IsNullOrEmpty(transactionType) || r.Type == transactionType) &&
                (!departmentId.HasValue || r.DepartmentId == departmentId.Value));

            var reportText = $"تقرير مالي من {startDate:yyyy-MM-dd} إلى {endDate:yyyy-MM-dd}\n";
            reportText += $"عدد السجلات: {filtered.Count()}\n";
            reportText += $"الإجمالي: {filtered.Sum(r => r.Amount):N2}";

            return System.Text.Encoding.UTF8.GetBytes(reportText);
        }

        public async Task<List<FinanceRecordDto>> GetAllFinanceRecordsAsync()
        {
            var records = await _financeRepository.GetAllAsync();
            return _mapper.Map<List<FinanceRecordDto>>(records);
        }

        public async Task<FinanceRecordDto> GetFinanceRecordByIdAsync(int id)
        {
            var record = await _financeRepository.GetByIdAsync(id);
            return record != null ? _mapper.Map<FinanceRecordDto>(record) : null;
        }

        public async Task<FinanceRecordDto> CreateFinanceRecordAsync(FinanceRecordDto financeDto)
        {
            var record = _mapper.Map<FinanceRecord>(financeDto);
            record.Date = DateTime.Now;

            await _financeRepository.AddAsync(record);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<FinanceRecordDto>(record);
        }

        public async Task UpdateFinanceRecordAsync(int id, FinanceRecordDto financeDto)
        {
            var existing = await _financeRepository.GetByIdAsync(id);
            if (existing == null)
                throw new Exception($"Finance record with ID {id} not found");

            _mapper.Map(financeDto, existing);
            _financeRepository.Update(existing);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteFinanceRecordAsync(int id)
        {
            var record = await _financeRepository.GetByIdAsync(id);
            if (record == null)
                throw new Exception($"Finance record with ID {id} not found");

            _financeRepository.Delete(record);
            await _unitOfWork.CommitAsync();
        }

        public async Task<decimal> GetTotalIncomeAsync()
        {
            var records = await _financeRepository.GetAllAsync(r => r.Type == "دخل");
            return records.Sum(r => r.Amount);
        }

        public async Task<decimal> GetTotalExpensesAsync()
        {
            var records = await _financeRepository.GetAllAsync(r => r.Type == "مصاريف");
            return records.Sum(r => r.Amount);
        }
        public async Task<List<StudentPaymentDto>> GetRecentPaymentsAsync(int count)
        {
            var payments = await _paymentRepository.GetAllAsync();
            var recentPayments = payments
                .OrderByDescending(p => p.PaymentDate)
                .Take(count)
                .ToList();

            return _mapper.Map<List<StudentPaymentDto>>(recentPayments);
        }
        public async Task<FinanceSummaryDto> GetFinanceSummaryAsync()
        {
            var records = await _financeRepository.GetAllAsync();

            var totalIncome = records.Where(r => r.Type == "دخل").Sum(r => r.Amount);
            var totalExpenses = records.Where(r => r.Type == "مصاريف").Sum(r => r.Amount);

            return new FinanceSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                Balance = totalIncome - totalExpenses
            };
        }

        public Task<List<EmployeeSalaryDto>> GetRecentSalariesAsync(int count = 10)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GenerateFinancialReportAsync(string reportType, DateTime startDate, DateTime endDate, int departmentId, string transactionType)
        {
            throw new NotImplementedException();
        }

        public Task<List<IFinanceServices.MonthlyFinancialData>> GetMonthlyFinancialDataAsync(int year)
        {
            throw new NotImplementedException();
        }
        public async Task<RevenueDataDto> GetRevenueDataAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            // تحديد الفترة الزمنية إذا لم يتم توفيرها
            var effectiveStartDate = startDate ?? DateTime.Now.AddMonths(-1); // آخر شهر كافتراضي
            var effectiveEndDate = endDate ?? DateTime.Now;

            try
            {
                // جلب سجلات الإيرادات في الفترة المحددة
                var revenueRecords = await _financeRepository.GetAllAsync(
                    r => r.Type == "دخل" &&
                         r.Date >= effectiveStartDate &&
                         r.Date <= effectiveEndDate);

                // جلب سجلات المدفوعات الطلابية في الفترة المحددة
                var paymentRecords = await _paymentRepository.GetAllAsync(
                    p => p.PaymentDate >= effectiveStartDate &&
                         p.PaymentDate <= effectiveEndDate);

                // حساب الإجماليات
                var totalRevenue = revenueRecords.Sum(r => r.Amount) + paymentRecords.Sum(p => p.Amount);

                return new RevenueDataDto
                {
                    Total = totalRevenue,
                    StartDate = effectiveStartDate,
                    EndDate = effectiveEndDate,
                    RevenueRecords = _mapper.Map<List<FinanceRecordDto>>(revenueRecords),
                    PaymentRecords = _mapper.Map<List<StudentPaymentDto>>(paymentRecords)
                };
            }
            catch (Exception ex)
            {
                // يمكنك استخدام نظام التسجيل (Logging) هنا
                throw new ApplicationException("حدث خطأ أثناء جلب بيانات الإيرادات", ex);
            }
        }
    }
}
