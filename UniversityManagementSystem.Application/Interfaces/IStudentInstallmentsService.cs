using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IStudentInstallmentsService
    {
        Task<PaginatedResult<StudentInstallmentDto>> GetStudentInstallmentsAsync(int pageNumber, int pageSize, string searchTerm = "", string program = "", PaymentStatus? status = null);
        Task<List<StudentInstallmentDto>> GetAllStudentInstallmentsAsync();
        Task<StudentInstallmentDto> GetStudentInstallmentByIdAsync(int installmentId);
        Task<StudentInstallmentDto> AddStudentInstallmentAsync(StudentInstallmentDto installmentDto);
        Task UpdateStudentInstallmentAsync(int installmentId, StudentInstallmentDto installmentDto);
        Task DeleteStudentInstallmentAsync(int installmentId);
        Task<StudentInstallmentStatsDto> GetInstallmentStatsAsync();
        Task<List<InstallmentPaymentDto>> GetInstallmentPaymentsAsync(int studentId);
        Task<InstallmentPaymentDto> AddInstallmentPaymentAsync(InstallmentPaymentDto paymentDto);
        Task<byte[]> ExportInstallmentsToExcelAsync(DateTime fromDate, DateTime toDate);
        Task<byte[]> ExportInstallmentsToPdfAsync(DateTime fromDate, DateTime toDate);
        Task<List<StudentInstallmentDto>> GetStudentsByProgramAsync(string program);
        Task<decimal> GetTotalOutstandingAmountAsync();
        Task<decimal> GetTotalCollectedAmountAsync(DateTime fromDate, DateTime toDate);
        Task<List<StudentInstallmentDto>> GetOverdueInstallmentsAsync();
        Task<List<StudentInstallmentDto>> GetUpcomingInstallmentsAsync();
        Task<List<InstallmentHistoryDto>> GetStudentInstallmentHistoryAsync(int studentId);
        Task<StudentInstallmentDto> GenerateNextInstallmentAsync(int studentId);
    }
}
