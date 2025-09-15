
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
    public class StudentInstallmentsService : Injectable, IStudentInstallmentsService
    {
        private readonly IRepository<StudentInstallment> _installmentRepository;
        private readonly IRepository<InstallmentPayment> _paymentRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StudentInstallmentsService(
            IRepository<StudentInstallment> installmentRepository,
            IRepository<InstallmentPayment> paymentRepository,
            IRepository<Student> studentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _installmentRepository = installmentRepository;
            _paymentRepository = paymentRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<StudentInstallmentDto>> GetStudentInstallmentsAsync(int pageNumber, int pageSize, string searchTerm = "", string program = "", PaymentStatus? status = null)
        {
            var installments = await _installmentRepository.GetAllAsync();
            var query = installments.AsQueryable();

            // تطبيق الفلاتر
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(s => s.StudentName.Contains(searchTerm) ||
                                      s.StudentId.Contains(searchTerm) ||
                                      s.Program.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(program))
            {
                query = query.Where(s => s.Program == program);
            }

            if (status.HasValue)
            {
                query = query.Where(s => s.Status == status.Value);
            }

            var filteredInstallments = query.Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToList();

            var totalCount = query.Count();

            return new PaginatedResult<StudentInstallmentDto>
            {
                Data = _mapper.Map<List<StudentInstallmentDto>>(filteredInstallments),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<List<StudentInstallmentDto>> GetAllStudentInstallmentsAsync()
        {
            var installments = await _installmentRepository.GetAllAsync();
            return _mapper.Map<List<StudentInstallmentDto>>(installments.OrderBy(s => s.StudentName));
        }

        public async Task<StudentInstallmentDto> GetStudentInstallmentByIdAsync(int installmentId)
        {
            var installment = await _installmentRepository.GetByIdAsync(installmentId);
            if (installment == null)
                throw new Exception("قسط الطالب غير موجود");

            return _mapper.Map<StudentInstallmentDto>(installment);
        }

        public async Task<StudentInstallmentDto> AddStudentInstallmentAsync(StudentInstallmentDto installmentDto)
        {
            // التحقق من صحة البيانات
            if (string.IsNullOrWhiteSpace(installmentDto.StudentName))
                throw new Exception("اسم الطالب مطلوب");

            if (string.IsNullOrWhiteSpace(installmentDto.StudentId))
                throw new Exception("رقم الطالب مطلوب");

            if (installmentDto.Amount <= 0)
                throw new Exception("مبلغ القسط يجب أن يكون أكبر من صفر");

            var installment = _mapper.Map<StudentInstallment>(installmentDto);
            installment.CreatedAt = DateTime.Now;
            installment.UpdatedAt = DateTime.Now;
            installment.OutstandingAmount = CalculateOutstandingAmount(installment);

            await _installmentRepository.AddAsync(installment);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<StudentInstallmentDto>(installment);
        }

        public async Task UpdateStudentInstallmentAsync(int installmentId, StudentInstallmentDto installmentDto)
        {
            var existingInstallment = await _installmentRepository.GetByIdAsync(installmentId);
            if (existingInstallment == null)
                throw new Exception("قسط الطالب غير موجود");

            _mapper.Map(installmentDto, existingInstallment);
            existingInstallment.UpdatedAt = DateTime.Now;
            existingInstallment.OutstandingAmount = CalculateOutstandingAmount(existingInstallment);

            _installmentRepository.Update(existingInstallment);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteStudentInstallmentAsync(int installmentId)
        {
            var installment = await _installmentRepository.GetByIdAsync(installmentId);
            if (installment == null)
                throw new Exception("قسط الطالب غير موجود");

            _installmentRepository.Delete(installment);
            await _unitOfWork.CommitAsync();
        }

        public async Task<StudentInstallmentStatsDto> GetInstallmentStatsAsync()
        {
            var installments = await _installmentRepository.GetAllAsync();
            var payments = await _paymentRepository.GetAllAsync();

            var totalStudents = installments.Select(i => i.StudentId).Distinct().Count();
            var totalInstallments = installments.Count();
            var paidInstallments = installments.Count(i => i.Status == PaymentStatus.Paid);
            var pendingInstallments = installments.Count(i => i.Status == PaymentStatus.Pending);
            var overdueInstallments = installments.Count(i => i.DueDate < DateTime.Now && i.Status != PaymentStatus.Paid);

            var totalAmount = installments.Sum(i => i.Amount);
            var collectedAmount = payments.Sum(p => p.Amount);
            var outstandingAmount = installments.Sum(i => i.OutstandingAmount);

            var programs = installments.Select(i => i.Program).Distinct().Count();

            return new StudentInstallmentStatsDto
            {
                TotalStudents = totalStudents,
                TotalInstallments = totalInstallments,
                PaidInstallments = paidInstallments,
                PendingInstallments = pendingInstallments,
                OverdueInstallments = overdueInstallments,
                TotalAmount = totalAmount,
                CollectedAmount = collectedAmount,
                OutstandingAmount = outstandingAmount,
                ProgramsCount = programs
            };
        }

        public async Task<List<InstallmentPaymentDto>> GetInstallmentPaymentsAsync(int studentId)
        {
            var payments = await _paymentRepository.GetAllAsync();
            var studentPayments = payments.Where(p => p.StudentInstallmentId == studentId)
                                        .OrderByDescending(p => p.PaymentDate)
                                        .ToList();

            return _mapper.Map<List<InstallmentPaymentDto>>(studentPayments);
        }

        public async Task<InstallmentPaymentDto> AddInstallmentPaymentAsync(InstallmentPaymentDto paymentDto)
        {
            var payment = _mapper.Map<InstallmentPayment>(paymentDto);
            payment.PaymentDate = DateTime.Now;
            payment.CreatedAt = DateTime.Now;

            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.CommitAsync();

            // تحديث حالة القسط
            var installment = await _installmentRepository.GetByIdAsync(paymentDto.StudentInstallmentId);
            if (installment != null)
            {
                installment.OutstandingAmount = CalculateOutstandingAmount(installment);
                installment.Status = installment.OutstandingAmount <= 0 ? PaymentStatus.Paid : PaymentStatus.Partial;
                installment.LastPaymentDate = payment.PaymentDate;
                installment.UpdatedAt = DateTime.Now;

                _installmentRepository.Update(installment);
                await _unitOfWork.CommitAsync();
            }

            return _mapper.Map<InstallmentPaymentDto>(payment);
        }

        public async Task<byte[]> ExportInstallmentsToExcelAsync(DateTime fromDate, DateTime toDate)
        {
            var installments = await GetAllStudentInstallmentsAsync();
            // تنفيذ التصدير إلى Excel
            return new byte[0];
        }

        public async Task<byte[]> ExportInstallmentsToPdfAsync(DateTime fromDate, DateTime toDate)
        {
            var stats = await GetInstallmentStatsAsync();
            var installments = await GetAllStudentInstallmentsAsync();
            // تنفيذ التصدير إلى PDF
            return new byte[0];
        }

        public async Task<List<StudentInstallmentDto>> GetStudentsByProgramAsync(string program)
        {
            var installments = await _installmentRepository.GetAllAsync();
            var programInstallments = installments.Where(s => s.Program == program)
                                                .OrderBy(s => s.StudentName)
                                                .ToList();

            return _mapper.Map<List<StudentInstallmentDto>>(programInstallments);
        }

        public async Task<decimal> GetTotalOutstandingAmountAsync()
        {
            var installments = await _installmentRepository.GetAllAsync();
            return installments.Sum(i => i.OutstandingAmount);
        }

        public async Task<decimal> GetTotalCollectedAmountAsync(DateTime fromDate, DateTime toDate)
        {
            var payments = await _paymentRepository.GetAllAsync();
            return payments.Where(p => p.PaymentDate >= fromDate && p.PaymentDate <= toDate)
                         .Sum(p => p.Amount);
        }

        public async Task<List<StudentInstallmentDto>> GetOverdueInstallmentsAsync()
        {
            var installments = await _installmentRepository.GetAllAsync();
            var overdueInstallments = installments.Where(i => i.DueDate < DateTime.Now &&
                                                          i.Status != PaymentStatus.Paid)
                                                .OrderBy(i => i.DueDate)
                                                .ToList();

            return _mapper.Map<List<StudentInstallmentDto>>(overdueInstallments);
        }

        public async Task<List<StudentInstallmentDto>> GetUpcomingInstallmentsAsync()
        {
            var installments = await _installmentRepository.GetAllAsync();
            var upcomingInstallments = installments.Where(i => i.DueDate >= DateTime.Now &&
                                                           i.DueDate <= DateTime.Now.AddDays(30) &&
                                                           i.Status != PaymentStatus.Paid)
                                                 .OrderBy(i => i.DueDate)
                                                 .ToList();

            return _mapper.Map<List<StudentInstallmentDto>>(upcomingInstallments);
        }

        public async Task<List<InstallmentHistoryDto>> GetStudentInstallmentHistoryAsync(int studentId)
        {
            var installments = await _installmentRepository.GetAllAsync();
            var studentInstallments = installments.Where(i => i.StudentId == studentId.ToString())
                                                .OrderByDescending(i => i.CreatedAt)
                                                .ToList();

            return _mapper.Map<List<InstallmentHistoryDto>>(studentInstallments);
        }

        public async Task<StudentInstallmentDto> GenerateNextInstallmentAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                throw new Exception("الطالب غير موجود");

            // هنا يمكنك تنفيذ منطق إنشاء القسط التالي بناءً على خطة الدفع
            var nextInstallment = new StudentInstallment
            {
                StudentId = student.Id.ToString(),
                StudentName = student.FullName,
                Program = student.Program,
                Amount = 0, // سيتم تحديده لاحقاً
                DueDate = DateTime.Now.AddMonths(1),
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _installmentRepository.AddAsync(nextInstallment);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<StudentInstallmentDto>(nextInstallment);
        }

        private decimal CalculateOutstandingAmount(StudentInstallment installment)
        {
            var payments = _paymentRepository.GetAllAsync().Result
                          .Where(p => p.StudentInstallmentId == installment.Id)
                          .Sum(p => p.Amount);

            return installment.Amount - payments;
        }
    }
}
