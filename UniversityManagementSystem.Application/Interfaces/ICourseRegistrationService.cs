using System;
using System.Collections.Generic;
using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface ICourseRegistrationService
    {
        Task<List<CourseRegistrationDto>> GetAllRegistrationsAsync();
        Task<List<CourseRegistrationDto>> GetRegistrationsByStudentIdAsync(string studentId);
        Task<List<CourseRegistrationDto>> GetRegistrationsByCourseIdAsync(int courseId);
        Task<List<CourseRegistrationDto>> GetRegistrationsByStatusAsync(string status);
        Task<CourseRegistrationDto?> GetRegistrationByIdAsync(int id);
        Task<CourseRegistrationDto> CreateRegistrationAsync(CourseRegistrationDto registrationDto);
        Task UpdateRegistrationAsync(int id, CourseRegistrationDto registrationDto);
        Task DeleteRegistrationAsync(int id);
        Task<bool> RegistrationExistsAsync(int id);
        Task<decimal> GetTotalAmountPaidByStudentAsync(string studentId);
        Task<int> GetStudentRegistrationCountAsync(string studentId);
        Task<int> GetCourseRegistrationCountAsync(int courseId);
        Task<List<CourseRegistrationDto>> GetRecentRegistrationsAsync(int count);
        Task<decimal> GetTotalRevenueAsync();
        Task<List<CourseRegistrationDto>> GetRegistrationsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task CancelRegistrationAsync(int id);
        Task CompleteRegistrationAsync(int id);
        Task<bool> IsStudentRegisteredAsync(string studentId, int courseId);
    }
}
