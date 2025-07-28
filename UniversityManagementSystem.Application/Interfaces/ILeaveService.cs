using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface ILeaveService
    {
        Task<List<LeaveDto>> GetAllLeavesAsync();
        Task<List<LeaveDto>> GetLeavesByEmployeeAsync(int employeeId);
        Task<List<LeaveDto>> GetLeavesByStatusAsync(string status);
        Task<List<LeaveDto>> GetLeavesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<LeaveDto?> GetLeaveByIdAsync(int id);
        Task<LeaveDto> CreateLeaveAsync(LeaveDto leaveDto);
        Task UpdateLeaveAsync(int id, LeaveDto leaveDto);
        Task DeleteLeaveAsync(int id);
        Task<LeaveDto> ApproveLeaveAsync(int id, string approvedBy);
        Task<LeaveDto> RejectLeaveAsync(int id, string reason);
        Task<LeaveSummaryDto> GetLeaveSummaryAsync();
        Task<List<LeaveDto>> GetPendingLeavesAsync();
        Task<int> GetEmployeeLeaveBalanceAsync(int employeeId, int leaveTypeId);
        Task<bool> CheckLeaveConflictAsync(int employeeId, DateTime startDate, DateTime endDate);
    }
}
