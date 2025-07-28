using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeDto>> GetAllLeaveTypesAsync();
        Task<LeaveTypeDto> GetLeaveTypeByIdAsync(int id);
        Task<LeaveTypeDto> CreateLeaveTypeAsync(LeaveTypeDto leaveTypeDto);
        Task UpdateLeaveTypeAsync(int id, LeaveTypeDto leaveTypeDto);
        Task DeleteLeaveTypeAsync(int id);
        Task<bool> LeaveTypeExistsAsync(int id);
        Task<List<LeaveTypeDto>> GetActiveLeaveTypesAsync();
        Task<LeaveTypeDto> GetLeaveTypeByNameAsync(string name);
    }
}
