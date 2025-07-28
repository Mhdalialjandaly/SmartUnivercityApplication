using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IStudentPaymentService
    {
        Task<List<StudentPaymentDto>> GetAllPaymentsAsync();
        Task<List<StudentPaymentDto>> GetPaymentsByStudentAsync(string studentId);
        Task<List<StudentPaymentDto>> GetPaymentsByCourseAsync(int courseId);
        Task<StudentPaymentDto?> GetPaymentByIdAsync(int id);
        Task<StudentPaymentDto> CreatePaymentAsync(StudentPaymentDto paymentDto);
        Task UpdatePaymentAsync(int id, StudentPaymentDto paymentDto);
        Task DeletePaymentAsync(int id);
        Task<StudentPaymentSummaryDto> GetPaymentSummaryAsync();
        Task<List<StudentPaymentDto>> GetPendingPaymentsAsync();
        Task<decimal> GetStudentTotalPaymentsAsync(string studentId);
        Task<decimal> GetCourseTotalPaymentsAsync(int courseId);
    }
}
