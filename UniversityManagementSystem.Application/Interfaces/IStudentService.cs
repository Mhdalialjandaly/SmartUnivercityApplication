using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentDto>> GetAllStudentsAsync();
        Task<List<StudentDto>> GetStudentsByDepartmentAsync(int departmentId);
        Task<List<StudentDto>> GetStudentsByStatusAsync(string status);
        Task<StudentDto?> GetStudentByIdAsync(int id);
        Task<StudentDto> CreateStudentAsync(StudentDto studentDto);
        Task UpdateStudentAsync(int id, StudentDto studentDto);
        Task DeleteStudentAsync(int id);
        Task<bool> StudentExistsAsync(int id);
        Task<StudentDto?> GetStudentByStudentIdAsync(string studentId);
        Task<List<StudentDto>> GetActiveStudentsAsync();
        Task<int> GetTotalStudentsCountAsync();
        Task<int> GetActiveStudentsCountAsync();
        Task<int> GetNewStudentsCountAsync();
        Task<decimal> GetAverageGPAAsync();
        Task<decimal> GetStudentAccountBalanceAsync(int studentId);
        Task UpdateStudentAccountBalanceAsync(int studentId, decimal newBalance);
        Task<List<StudentDto>> SearchStudentsAsync(string searchTerm);
        Task<StudentSearchResult> GetStudentsAsync(int pageNumber, int pageSize, string searchTerm, int? departmentId, StudentStatus status);
    }
}
