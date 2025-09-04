using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IStudentServices
    {
        Task<PaginatedResult<Student>> GetStudentsPagedAsync(int pageNumber, int pageSize,string term,int? departmentId,StudentStatus status);

        Task<StudentDto> GetStudentByIdAsync(int studentId);
        Task<List<StudentDto>> GetStudentsAsync(string term);
        Task<List<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> CreateStudentAsync(StudentDto studentDto);
        Task UpdateStudentAsync(int studentId, StudentDto studentDto);
        Task DeleteStudentAsync(int studentId);
        Task<bool> StudentExistsAsync(int studentId);

        // Additional business logic methods
        Task<decimal> GetStudentAccountBalanceAsync(int studentId);
        Task<bool> UpdateStudentAccountBalanceAsync(int studentId, decimal amount);
        Task<bool> ChangeStudentStatusAsync(int studentId, StudentStatus status);
        Task<List<CourseRegistrationDto>> GetStudentCoursesAsync(int studentId);
        Task<List<StudentDocumentDto>> GetStudentDocumentsAsync(int studentId);
        Task<bool> CompleteRegistrationAsync(int studentId);
        Task<int> GetTotalStudentsCountAsync();
        Task<int> GetActiveStudentsCountAsync();
        Task<int> GetStudentsCountByGenderAsync(bool isMale);
        Task<int> GetNewStudentsCountAsync();
        Task<double> GetAverageGPAAsync();
        Task<List<InvoiceDto>> GetStudentInvoicesAsync(int studentId);
        Task<int> GetNewStudentsCountAsync(DateTime? startDate = null, DateTime? endDate = null, string academicYear = null);
        Task<List<StudentCountryDistributionDto>> GetStudentsByCountryAsync(
        DateTime? startDate = null,
        DateTime? endDate = null,
        string academicYear = null);
    }
}
