using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly IRepository<Leave> _leaveRepository;
        private readonly IRepository<LeaveType> _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LeaveService(
            IRepository<Leave> leaveRepository,
            IRepository<LeaveType> leaveTypeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _leaveRepository = leaveRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LeaveDto>> GetAllLeavesAsync()
        {
            var leaves = await _leaveRepository.GetAllAsync(l => l.LeaveType);
            return _mapper.Map<List<LeaveDto>>(leaves);
        }

        public async Task<List<LeaveDto>> GetLeavesByEmployeeAsync(int employeeId)
        {
            var leaves = await _leaveRepository.GetAllAsync(l => l.EmployeeId == employeeId, l => l.LeaveType);
            return _mapper.Map<List<LeaveDto>>(leaves);
        }

        public async Task<List<LeaveDto>> GetLeavesByStatusAsync(string status)
        {
            var leaves = await _leaveRepository.GetAllAsync(l => l.Status == status, l => l.LeaveType);
            return _mapper.Map<List<LeaveDto>>(leaves);
        }

        public async Task<List<LeaveDto>> GetLeavesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var leaves = await _leaveRepository.GetAllAsync(
                l => l.StartDate <= endDate && l.EndDate >= startDate,
                l => l.LeaveType);
            return _mapper.Map<List<LeaveDto>>(leaves);
        }

        public async Task<LeaveDto?> GetLeaveByIdAsync(int id)
        {
            var leave = await _leaveRepository.GetByIdAsync(id, l => l.LeaveType);
            return leave != null ? _mapper.Map<LeaveDto>(leave) : null;
        }

        public async Task<LeaveDto> CreateLeaveAsync(LeaveDto leaveDto)
        {
            var leave = _mapper.Map<Leave>(leaveDto);
            leave.TotalDays = (leave.EndDate - leave.StartDate).Days + 1;

            await _leaveRepository.AddAsync(leave);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<LeaveDto>(leave);
        }

        public async Task UpdateLeaveAsync(int id, LeaveDto leaveDto)
        {
            var existingLeave = await _leaveRepository.GetByIdAsync(id);
            if (existingLeave == null)
                throw new Exception($"Leave with ID {id} not found");

            _mapper.Map(leaveDto, existingLeave);
            existingLeave.TotalDays = (existingLeave.EndDate - existingLeave.StartDate).Days + 1;

            _leaveRepository.Update(existingLeave);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteLeaveAsync(int id)
        {
            var leave = await _leaveRepository.GetByIdAsync(id);
            if (leave == null)
                throw new Exception($"Leave with ID {id} not found");

            _leaveRepository.Delete(leave);
            await _unitOfWork.CommitAsync();
        }

        public async Task<LeaveDto> ApproveLeaveAsync(int id, string approvedBy)
        {
            var leave = await _leaveRepository.GetByIdAsync(id);
            if (leave == null)
                throw new Exception($"Leave with ID {id} not found");

            leave.Status = "مقبول";
            leave.ApprovedDate = DateTime.Now;
            leave.ApprovedBy = approvedBy;

            _leaveRepository.Update(leave);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<LeaveDto>(leave);
        }

        public async Task<LeaveDto> RejectLeaveAsync(int id, string reason)
        {
            var leave = await _leaveRepository.GetByIdAsync(id);
            if (leave == null)
                throw new Exception($"Leave with ID {id} not found");

            leave.Status = "مرفوض";
            leave.Notes = reason;

            _leaveRepository.Update(leave);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<LeaveDto>(leave);
        }

        public async Task<LeaveSummaryDto> GetLeaveSummaryAsync()
        {
            var leaves = await _leaveRepository.GetAllAsync();

            return new LeaveSummaryDto
            {
                TotalLeaves = leaves.Count(),
                PendingLeaves = leaves.Count(l => l.Status == "معلق"),
                ApprovedLeaves = leaves.Count(l => l.Status == "مقبول"),
                RejectedLeaves = leaves.Count(l => l.Status == "مرفوض"),
                TotalLeaveDays = leaves.Sum(l => l.TotalDays)
            };
        }

        public async Task<List<LeaveDto>> GetPendingLeavesAsync()
        {
            var leaves = await _leaveRepository.GetAllAsync(l => l.Status == "معلق", l => l.LeaveType);
            return _mapper.Map<List<LeaveDto>>(leaves);
        }

        public async Task<int> GetEmployeeLeaveBalanceAsync(int employeeId, int leaveTypeId)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(leaveTypeId);
            if (leaveType == null)
                throw new Exception($"Leave type with ID {leaveTypeId} not found");

            var employeeLeaves = await _leaveRepository.GetAllAsync(
                l => l.EmployeeId == employeeId &&
                     l.LeaveTypeId == leaveTypeId &&
                     l.Status == "مقبول" &&
                     l.StartDate.Year == DateTime.Now.Year);

            var usedDays = employeeLeaves.Sum(l => l.TotalDays);
            return leaveType.MaxDaysPerYear - usedDays;
        }

        public async Task<bool> CheckLeaveConflictAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            var conflictingLeaves = await _leaveRepository.GetAllAsync(
                l => l.EmployeeId == employeeId &&
                     l.Status != "مرفوض" &&
                     ((l.StartDate <= startDate && l.EndDate >= startDate) ||
                      (l.StartDate <= endDate && l.EndDate >= endDate) ||
                      (l.StartDate >= startDate && l.EndDate <= endDate)));

            return conflictingLeaves.Any();
        }
    }
}
