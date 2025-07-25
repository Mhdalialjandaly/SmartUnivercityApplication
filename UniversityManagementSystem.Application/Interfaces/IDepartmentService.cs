using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> GetDepartmentByIdAsync(int id);
        Task<List<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto);
        Task UpdateDepartmentAsync(int id, DepartmentDto departmentDto);
        Task DeleteDepartmentAsync(int id);
        Task<bool> DepartmentExistsAsync(int id);

        // Department-specific methods
        Task<List<StudentDto>> GetDepartmentStudentsAsync(int departmentId);
        Task<List<CourseDto>> GetDepartmentCoursesAsync(int departmentId);
        Task<int> GetDepartmentStudentCountAsync(int departmentId);
        Task<decimal> GetDepartmentTotalAccountBalanceAsync(int departmentId);
    }
}
