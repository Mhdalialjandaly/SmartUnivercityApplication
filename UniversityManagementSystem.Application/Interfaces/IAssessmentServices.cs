using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IAssessmentServices 
    {
        Task<PaginatedResult<AssessmentDto>> GetAssessmentsAsync(int pageNumber, int pageSize, string searchTerm = "");
        Task<List<AssessmentDto>> GetAllAssessmentsAsync();
        Task<List<AssessmentDto>> SearchAssessmentsAsync(string searchTerm);
        Task<AssessmentDto> GetAssessmentByIdAsync(int id);
        Task<AssessmentDto> CreateAssessmentAsync(AssessmentDto assessmentDto);
        Task UpdateAssessmentAsync(int id, AssessmentDto assessmentDto);
        Task DeleteAssessmentAsync(int id);
        Task<AssessmentStatsDto> GetAssessmentStatsAsync();
        Task<List<AssessmentDto>> GetUpcomingAssessmentsAsync(int days = 7);
        Task<List<StudentAssessmentDto>> GetStudentAssessmentsAsync(int studentId);
        Task<bool> SubmitAssessmentResultAsync(int assessmentId, int studentId, decimal score, string feedback = "");
    }
}