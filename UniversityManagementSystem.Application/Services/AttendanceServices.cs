using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class AttendanceServices : Injectable, IAttendanceServices
    {
        private readonly IRepository<Attendance> _attendanceRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceServices(
            IRepository<Attendance> attendanceRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AttendanceDto>> GetAllAttendanceAsync()
        {
            var attendance = await _attendanceRepository.GetAllAsync();
            return _mapper.Map<List<AttendanceDto>>(attendance);
        }

        public async Task<List<AttendanceDto>> GetAttendanceByEmployeeAsync(string employeeId)
        {
            var attendance = await _attendanceRepository.GetAllAsync(a => a.EmployeeId == employeeId);
            return _mapper.Map<List<AttendanceDto>>(attendance);
        }

        public async Task<List<AttendanceDto>> GetAttendanceByDateAsync(DateTime date)
        {
            var attendance = await _attendanceRepository.GetAllAsync(a => a.Date.Date == date.Date);
            return _mapper.Map<List<AttendanceDto>>(attendance);
        }

        public async Task<List<AttendanceDto>> GetAttendanceByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var attendance = await _attendanceRepository.GetAllAsync(a => a.Date >= startDate && a.Date <= endDate);
            return _mapper.Map<List<AttendanceDto>>(attendance);
        }

        public async Task<AttendanceDto?> GetAttendanceByIdAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            return attendance != null ? _mapper.Map<AttendanceDto>(attendance) : null;
        }

        public async Task<AttendanceDto> CreateAttendanceAsync(AttendanceDto attendanceDto)
        {
            var attendance = _mapper.Map<Attendance>(attendanceDto);

            // حساب ساعات العمل إذا كان الوقت متوفر
            if (attendance.CheckInTime.HasValue && attendance.CheckOutTime.HasValue)
            {
                attendance.HoursWorked = await CalculateHoursWorkedAsync(attendance.CheckInTime, attendance.CheckOutTime);
            }

            await _attendanceRepository.AddAsync(attendance);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<AttendanceDto>(attendance);
        }

        public async Task UpdateAttendanceAsync(int id, AttendanceDto attendanceDto)
        {
            var existingAttendance = await _attendanceRepository.GetByIdAsync(id);
            if (existingAttendance == null)
                throw new Exception($"Attendance with ID {id} not found");

            _mapper.Map(attendanceDto, existingAttendance);

            // حساب ساعات العمل إذا كان الوقت متوفر
            if (existingAttendance.CheckInTime.HasValue && existingAttendance.CheckOutTime.HasValue)
            {
                existingAttendance.HoursWorked = await CalculateHoursWorkedAsync(
                    existingAttendance.CheckInTime,
                    existingAttendance.CheckOutTime);
            }

            _attendanceRepository.Update(existingAttendance);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAttendanceAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
                throw new Exception($"Attendance with ID {id} not found");

            _attendanceRepository.Delete(attendance);
            await _unitOfWork.CommitAsync();
        }

        public async Task<AttendanceSummaryDto> GetAttendanceSummaryAsync(DateTime startDate, DateTime endDate)
        {
            var attendance = await _attendanceRepository.GetAllAsync(a => a.Date >= startDate && a.Date <= endDate);

            return new AttendanceSummaryDto
            {
                TotalEmployees = attendance.Select(a => a.EmployeeId).Distinct().Count(),
                PresentCount = attendance.Count(a => a.Status == "حاضر"),
                AbsentCount = attendance.Count(a => a.Status == "غائب"),
                LeaveCount = attendance.Count(a => a.Status == "إجازة"),
                AverageHours = (decimal)(attendance.Any() ? attendance.Average(a => (double)a.HoursWorked) : 0) 
            };
        }

        public async Task<decimal> CalculateHoursWorkedAsync(DateTime? checkIn, DateTime? checkOut)
        {
            if (checkIn.HasValue && checkOut.HasValue)
            {
                var hours = (checkOut.Value - checkIn.Value).TotalHours;
                return Math.Round((decimal)hours, 2);
            }
            return 0;
        }

        public async Task<List<AttendanceDto>> GetAbsentEmployeesAsync(DateTime date)
        {
            var attendance = await _attendanceRepository.GetAllAsync(a => a.Date.Date == date.Date && a.Status == "غائب");
            return _mapper.Map<List<AttendanceDto>>(attendance);
        }
    }
}
