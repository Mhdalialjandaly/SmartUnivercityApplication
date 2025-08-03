using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class EmployeeService : Injectable, IEmployeeService
    {
        private readonly IRepository<Employee> _repository;
        private readonly IRepository<Department> _departmentRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<Employee> repository, IRepository<Department> departmentRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _departmentRepo = departmentRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeDto dto)
        {
            var entity = _mapper.Map<Employee>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<EmployeeDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _repository.Delete(entity);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var employees =_mapper.Map<List<EmployeeDto>>( await _repository.GetAllAsync(x => x.Department));
            return employees;
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id,x => x.Department);
            var dto = _mapper.Map<EmployeeDto>(employee);
            return dto;
        }

        public async Task UpdateAsync(EmployeeDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity != null)
            {
                _mapper.Map(dto, entity);
                _repository.Update(entity);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
