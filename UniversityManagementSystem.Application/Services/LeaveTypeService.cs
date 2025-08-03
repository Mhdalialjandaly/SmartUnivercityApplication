using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class LeaveTypeService : Injectable, ILeaveTypeService
    {
        private readonly IRepository<LeaveType> _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LeaveTypeService(
            IRepository<LeaveType> leaveTypeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LeaveTypeDto>> GetAllLeaveTypesAsync()
        {
            var leaveTypes = await _leaveTypeRepository.GetAllAsync();
            return _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        }

        public async Task<LeaveTypeDto?> GetLeaveTypeByIdAsync(int id)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
            return leaveType != null ? _mapper.Map<LeaveTypeDto>(leaveType) : null;
        }

        public async Task<LeaveTypeDto> CreateLeaveTypeAsync(LeaveTypeDto leaveTypeDto)
        {
            var leaveType = _mapper.Map<LeaveType>(leaveTypeDto);
            await _leaveTypeRepository.AddAsync(leaveType);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<LeaveTypeDto>(leaveType);
        }

        public async Task UpdateLeaveTypeAsync(int id, LeaveTypeDto leaveTypeDto)
        {
            var existingLeaveType = await _leaveTypeRepository.GetByIdAsync(id);
            if (existingLeaveType == null)
                throw new Exception($"Leave type with ID {id} not found");

            _mapper.Map(leaveTypeDto, existingLeaveType);
            _leaveTypeRepository.Update(existingLeaveType);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteLeaveTypeAsync(int id)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
            if (leaveType == null)
                throw new Exception($"Leave type with ID {id} not found");

            _leaveTypeRepository.Delete(leaveType);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> LeaveTypeExistsAsync(int id)
        {
            return await _leaveTypeRepository.ExistsAsync(lt => lt.Id == id);
        }

        public async Task<List<LeaveTypeDto>> GetActiveLeaveTypesAsync()
        {
            var leaveTypes = await _leaveTypeRepository.GetAllAsync(lt => lt.IsActive);
            return _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        }

        public async Task<LeaveTypeDto?> GetLeaveTypeByNameAsync(string name)
        {
            var leaveType = await _leaveTypeRepository.GetAllAsync(lt => lt.Name == name);
            return leaveType != null ? _mapper.Map<LeaveTypeDto>(leaveType) : null;
        }
    }
}
