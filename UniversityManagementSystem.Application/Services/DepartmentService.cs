using AutoMapper;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(
            IRepository<Department> departmentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id,d => d.Students);

            if (department == null)
                throw new Exception($"Department with ID {id} not found");

            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<List<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllAsync(
                 d => d.Students);

            return _mapper.Map<List<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            await _departmentRepository.AddAsync(department);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task UpdateDepartmentAsync(int id, DepartmentDto departmentDto)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(id);
            if (existingDepartment == null)
                throw new Exception($"Department with ID {id} not found");

            _mapper.Map(departmentDto, existingDepartment);
            _departmentRepository.Update(existingDepartment);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                throw new Exception($"Department with ID {id} not found");

            _departmentRepository.Delete(department);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> DepartmentExistsAsync(int id)
        {
            return await _departmentRepository.ExistsAsync(d => d.Id == id);
        }

        // Department-specific methods
        public async Task<List<StudentDto>> GetDepartmentStudentsAsync(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId,
                 d => d.Students);

            return _mapper.Map<List<StudentDto>>(department?.Students ?? new List<Student>());
        }

        public async Task<List<CourseDto>> GetDepartmentCoursesAsync(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId,
                 d => d.Courses);

            return _mapper.Map<List<CourseDto>>(department?.Courses ?? new List<Course>());
        }

        public async Task<int> GetDepartmentStudentCountAsync(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId,
                 d => d.Students);

            return department?.Students?.Count ?? 0;
        }

        public async Task<decimal> GetDepartmentTotalAccountBalanceAsync(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId,
                 d => d.Students);

            return department?.Students?.Sum(s => s.AccountBalance) ?? 0;
        }
    }
}
