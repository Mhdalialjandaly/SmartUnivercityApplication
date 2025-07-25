using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IStudentServices
    {
        Task<PaginatedResult<Student>> GetStudentsPagedAsync(int pageNumber, int pageSize,string term,int? departmentId,StudentStatus status);
        Task<StudentDto> GetStudentByIdAsync(string studentId);
        Task<List<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> CreateStudentAsync(StudentDto studentDto);
        Task UpdateStudentAsync(string studentId, StudentDto studentDto);
        Task DeleteStudentAsync(string studentId);
        Task<bool> StudentExistsAsync(string studentId);

        // Additional business logic methods
        Task<decimal> GetStudentAccountBalanceAsync(string studentId);
        Task<bool> UpdateStudentAccountBalanceAsync(string studentId, decimal amount);
        Task<bool> ChangeStudentStatusAsync(string studentId, StudentStatus status);
        Task<List<CourseRegistration>> GetStudentCoursesAsync(string studentId);
        Task<List<StudentDocument>> GetStudentDocumentsAsync(string studentId);
        Task<bool> CompleteRegistrationAsync(string studentId);
        Task<int> GetTotalStudentsCountAsync();
        Task<int> GetActiveStudentsCountAsync();
        Task<int> GetNewStudentsCountAsync();
        Task<double> GetAverageGPAAsync();
        Task<List<InvoiceDto>> GetStudentInvoicesAsync(string studentId);
    }
}
