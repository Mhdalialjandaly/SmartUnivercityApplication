using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class StudentPaymentService : Injectable, IStudentPaymentService
    {
        private readonly IRepository<StudentPayment> _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StudentPaymentService(
            IRepository<StudentPayment> paymentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StudentPaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return _mapper.Map<List<StudentPaymentDto>>(payments);
        }

        public async Task<List<StudentPaymentDto>> GetPaymentsByStudentAsync(string studentId)
        {
            var payments = await _paymentRepository.GetAllAsync(p => p.StudentId == studentId);
            return _mapper.Map<List<StudentPaymentDto>>(payments);
        }

        public async Task<List<StudentPaymentDto>> GetPaymentsByCourseAsync(int courseId)
        {
            var payments = await _paymentRepository.GetAllAsync(p => p.CourseId == courseId);
            return _mapper.Map<List<StudentPaymentDto>>(payments);
        }

        public async Task<StudentPaymentDto?> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            return payment != null ? _mapper.Map<StudentPaymentDto>(payment) : null;
        }

        public async Task<StudentPaymentDto> CreatePaymentAsync(StudentPaymentDto paymentDto)
        {
            var payment = _mapper.Map<StudentPayment>(paymentDto);
            payment.PaymentDate = DateTime.Now;

            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<StudentPaymentDto>(payment);
        }

        public async Task UpdatePaymentAsync(int id, StudentPaymentDto paymentDto)
        {
            var existingPayment = await _paymentRepository.GetByIdAsync(id);
            if (existingPayment == null)
                throw new Exception($"Payment with ID {id} not found");

            _mapper.Map(paymentDto, existingPayment);
            _paymentRepository.Update(existingPayment);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                throw new Exception($"Payment with ID {id} not found");

            _paymentRepository.Delete(payment);
            await _unitOfWork.CommitAsync();
        }

        public async Task<StudentPaymentSummaryDto> GetPaymentSummaryAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();

            return new StudentPaymentSummaryDto
            {
                TotalPayments = payments.Count(),
                TotalAmount = payments.Sum(p => p.Amount),
                PendingPayments = payments.Count(p => p.Status == "معلق"),
                PendingAmount = payments.Where(p => p.Status == "معلق").Sum(p => p.Amount)
            };
        }

        public async Task<List<StudentPaymentDto>> GetPendingPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllAsync(p => p.Status == "معلق");
            return _mapper.Map<List<StudentPaymentDto>>(payments);
        }

        public async Task<decimal> GetStudentTotalPaymentsAsync(string studentId)
        {
            var payments = await _paymentRepository.GetAllAsync(p => p.StudentId == studentId && p.Status == "مدفوع");
            return payments.Sum(p => p.Amount);
        }

        public async Task<decimal> GetCourseTotalPaymentsAsync(int courseId)
        {
            var payments = await _paymentRepository.GetAllAsync(p => p.CourseId == courseId && p.Status == "مدفوع");
            return payments.Sum(p => p.Amount);
        }
    }
}
