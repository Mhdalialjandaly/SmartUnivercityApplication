using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IProfessorServices
    {
        Task<ProfessorDto> GetProfessorByIdAsync(int professorId);
        Task<List<ProfessorDto>> GetAllProfessorsAsync();
        Task<ProfessorDto> CreateProfessorAsync(ProfessorDto professorDto);
        Task UpdateProfessorAsync(int professorId, ProfessorDto professorDto);
        Task DeleteProfessorAsync(int professorId);
        Task<bool> ProfessorExistsAsync(string employeeId);
        Task<List<CourseDto>> GetProfessorCoursesAsync(int professorId);
        Task<List<ScheduleDto>> GetProfessorScheduleAsync(int professorId);
        Task<bool> ChangeProfessorStatusAsync(int professorId, ProfessorStatus status);
        Task<int> GetTotalProfessorsCountAsync();
        Task<int> GetActiveProfessorsCountAsync();
        Task<int> GetNewProfessorsCountAsync();
        Task<double> GetAverageTeachingLoadAsync();
        Task<PaginatedResult<ProfessorDto>> GetProfessorsPagedAsync(
            int pageNumber,
            int pageSize,
            string searchTerm,
            int? departmentId,
            ProfessorStatus status);
        Task<List<WorkloadReportDto>> GetProfessorWorkloadReportAsync(int professorId);
    }
}
