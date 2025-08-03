using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class StudentApplicationsServices : Injectable, IStudentApplicationsServices
    {
        private readonly IRepository<StudentApplication> _applicationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentApplicationsServices(
            IRepository<StudentApplication> applicationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StudentApplicationDto>> GetAllApplicationsAsync()
        {
            var entities = await _applicationRepository.GetAllAsync();
            return _mapper.Map<List<StudentApplicationDto>>(entities);
        }

        public async Task<StudentApplicationDto> GetApplicationByIdAsync(int id)
        {
            var entity = await _applicationRepository.GetByIdAsync(id);
            return _mapper.Map<StudentApplicationDto>(entity);
        }

        public async Task<StudentApplicationDto> CreateApplicationAsync(StudentApplicationDto dto)
        {
            var entity = _mapper.Map<StudentApplication>(dto);
            await _applicationRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<StudentApplicationDto>(entity);
        }

        public async Task UpdateApplicationAsync(int id, StudentApplicationDto dto)
        {
            var entity = await _applicationRepository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("Application not found");

            _mapper.Map(dto, entity);
            _applicationRepository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteApplicationAsync(int id)
        {
            var entity = await _applicationRepository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("Application not found");

            _applicationRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> ApproveApplicationAsync(int id, string studentId)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            if (application == null)
                return false;

            application.Status = "مقبول";
            application.StudentId = studentId;
            _applicationRepository.Update(application);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> RejectApplicationAsync(int id, string reason)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            if (application == null)
                return false;

            application.Status = "مرفوض";
            application.RejectionReason = reason;
            _applicationRepository.Update(application);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<List<StudentApplicationDto>> GetPendingApplicationsAsync()
        {
            var entities = await _applicationRepository.FindAsync(x => x.Status == "قيد المراجعة");
            return _mapper.Map<List<StudentApplicationDto>>(entities);
        }

        public async Task<int> GetApplicationsCountAsync()
        {
            var list = await _applicationRepository.GetAllAsync();
            return list.Count();
        }

        public async Task<int> GetPendingCountAsync()
        {
            var list = await _applicationRepository.FindAsync(x => x.Status == "قيد المراجعة");
            return list.Count();
        }

        public async Task<int> GetApprovedCountAsync()
        {
            var list = await _applicationRepository.FindAsync(x => x.Status == "مقبول");
            return list.Count();
        }

        public async Task<int> GetRejectedCountAsync()
        {
            var list = await _applicationRepository.FindAsync(x => x.Status == "مرفوض");
            return list.Count();
        }
    }
}

