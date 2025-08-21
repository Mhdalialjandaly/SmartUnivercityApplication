using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IStudentPaymentServices
    {
        Task<List<StudentPaymentDto>> GetAllPaymentsAsync();
        Task<List<StudentPaymentDto>> GetPaymentsByStudentAsync(int studentId);
        Task<List<StudentPaymentDto>> GetPaymentsByCourseAsync(int courseId);
        Task<StudentPaymentDto> GetPaymentByIdAsync(int id);
        Task<StudentPaymentDto> CreatePaymentAsync(StudentPaymentDto paymentDto);
        Task UpdatePaymentAsync(int id, StudentPaymentDto paymentDto);
        Task DeletePaymentAsync(int id);
        Task<StudentPaymentSummaryDto> GetPaymentSummaryAsync();
        Task<List<StudentPaymentDto>> GetPendingPaymentsAsync();
        Task<decimal> GetStudentTotalPaymentsAsync(int studentId);
        Task<decimal> GetCourseTotalPaymentsAsync(int courseId);
        Task<PaginatedResult<StudentPaymentDto>> GetAllPaymentsAsync(
           int pageNumber = 1,
           int pageSize = 10,
           string searchTerm = "",
           string status = "");

        Task<List<StudentPaymentDto>> GetRecentPaymentsAsync(int count = 10);
    }
}
