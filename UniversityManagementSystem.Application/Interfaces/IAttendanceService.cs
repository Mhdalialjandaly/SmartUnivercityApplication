using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<List<AttendanceDto>> GetAllAttendanceAsync();
        Task<List<AttendanceDto>> GetAttendanceByEmployeeAsync(int employeeId);
        Task<List<AttendanceDto>> GetAttendanceByDateAsync(DateTime date);
        Task<List<AttendanceDto>> GetAttendanceByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<AttendanceDto?> GetAttendanceByIdAsync(int id);
        Task<AttendanceDto> CreateAttendanceAsync(AttendanceDto attendanceDto);
        Task UpdateAttendanceAsync(int id, AttendanceDto attendanceDto);
        Task DeleteAttendanceAsync(int id);
        Task<AttendanceSummaryDto> GetAttendanceSummaryAsync(DateTime startDate, DateTime endDate);
        Task<decimal> CalculateHoursWorkedAsync(DateTime? checkIn, DateTime? checkOut);
        Task<List<AttendanceDto>> GetAbsentEmployeesAsync(DateTime date);
    }
}
