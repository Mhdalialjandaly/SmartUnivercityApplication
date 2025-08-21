using AutoMapper;
using System.Linq.Expressions;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Enums;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class ProfessorServices : Injectable, IProfessorServices
    {
        private readonly IRepository<Professor> _professorRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Schedule> _scheduleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProfessorServices(
            IRepository<Professor> professorRepository,
            IRepository<Department> departmentRepository,
            IRepository<Course> courseRepository,
            IRepository<Schedule> scheduleRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _professorRepository = professorRepository;
            _departmentRepository = departmentRepository;
            _courseRepository = courseRepository;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProfessorDto> GetProfessorByIdAsync(int professorId)
        {
            var professor = await _professorRepository.GetByIdAsync(professorId,p => p.Department,x=>x.Courses);
            return _mapper.Map<ProfessorDto>(professor);
        }

        public async Task<List<ProfessorDto>> GetAllProfessorsAsync()
        {
            var professors = await _professorRepository.GetAllAsync(p => p.Department);
            return _mapper.Map<List<ProfessorDto>>(professors);
        }

        public async Task<ProfessorDto> CreateProfessorAsync(ProfessorDto professorDto)
        {
            var professor = _mapper.Map<Professor>(professorDto);
            await _professorRepository.AddAsync(professor);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ProfessorDto>(professor);
        }

        public async Task UpdateProfessorAsync(int professorId, ProfessorDto professorDto)
        {
            var existingProfessor = await _professorRepository.GetByIdAsync(professorId);
            if (existingProfessor == null)
                throw new Exception("Professor not found");

            _mapper.Map(professorDto, existingProfessor);
            _professorRepository.Update(existingProfessor);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteProfessorAsync(int professorId)
        {
            var professor = await _professorRepository.GetByIdAsync(professorId);
            if (professor == null)
                throw new Exception("Professor not found");

            _professorRepository.Delete(professor);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> ProfessorExistsAsync(string employeeId)
        {
            return await _professorRepository.ExistsAsync(p => p.EmployeeId == employeeId);
        }

        public async Task<List<CourseDto>> GetProfessorCoursesAsync(int professorId)
        {
            var professor = await _professorRepository.GetByIdAsync(professorId, p => p.Courses);
            return _mapper.Map<List<CourseDto>>(professor.Courses) ?? new List<CourseDto>();
        }

        public async Task<List<ScheduleDto>> GetProfessorScheduleAsync(int professorId)
        {
            // جلب الكورسات مع الجداول الزمنية في استعلام واحد
            var schedules = (await _courseRepository.FindAsync(
                c => c.ProfessorId == professorId && c.Schedules != null && c.Schedules.Any(),
                c => c.Schedules
            ))
            .SelectMany(c => c.Schedules)
            .DistinctBy(s => s.Id) 
            .ToList();

            return _mapper.Map<List<ScheduleDto>>(schedules) ?? new List<ScheduleDto>();
        }

        public async Task<bool> ChangeProfessorStatusAsync(int professorId, ProfessorStatus status)
        {
            var professor = await _professorRepository.GetByIdAsync(professorId);
            if (professor == null)
                return false;

            professor.Status = status;
            _professorRepository.Update(professor);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<int> GetTotalProfessorsCountAsync()
        {
            var professors = await _professorRepository.GetAllAsync();
            return professors.Count();
        }

        public async Task<int> GetActiveProfessorsCountAsync()
        {
            var professors = await _professorRepository.GetAllAsync();
            return professors.Count(p => p.Status == ProfessorStatus.Active);
        }

        public async Task<int> GetNewProfessorsCountAsync()
        {
            var professors = await _professorRepository.GetAllAsync();
            return professors.Count(p => p.HireDate > DateTime.Now.AddYears(-1));
        }

        public async Task<double> GetAverageTeachingLoadAsync()
        {
            var professors = await _professorRepository.GetAllAsync( p => p.Courses);
            return professors.Average(p => p.Courses?.Count ?? 0);
        }

        public async Task<PaginatedResult<ProfessorDto>> GetProfessorsPagedAsync(
            int pageNumber,
            int pageSize,
            string searchTerm,
            int? departmentId,
            ProfessorStatus status)
        {
            Expression<Func<Professor, bool>> filter = p =>
                (string.IsNullOrEmpty(searchTerm) ||
                 p.FullName.Contains(searchTerm) ||
                 p.EmployeeId.Contains(searchTerm)) &&
                (!departmentId.HasValue || p.DepartmentId == departmentId) &&
                (status == ProfessorStatus.All || p.Status == status);

            var professors = await _professorRepository.GetPagedAsync(
                pageNumber,
                pageSize);

            //var result = professors.Where(filter);
            //    filter,
            //    include: p => p.Include(x => x.Department),
            //    orderBy: q => q.OrderBy(x => x.FullName));

            return new PaginatedResult<ProfessorDto>
            {
                Data = _mapper.Map<List<ProfessorDto>>(professors),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = await _professorRepository.CountAsync(filter),
                TotalPages = (int)Math.Ceiling(await _professorRepository.CountAsync(filter) / (double)pageSize)
            };
        }

        public async Task<List<WorkloadReportDto>> GetProfessorWorkloadReportAsync(int professorId)
        {
            // جلب البروفيسور مع كورساته
            var professor = await _professorRepository.GetByIdAsync(professorId, p => p.Courses);
            if (professor == null || professor.Courses == null || !professor.Courses.Any())
            {
                return new List<WorkloadReportDto>();
            }

            // جلب الجداول الزمنية في استعلام واحد
            var courseIds = professor.Courses.Select(c => c.Id).ToList();
            var schedules = await _scheduleRepository.FindAsync(s => courseIds.Contains(s.CourseId));

            // إنشاء التقرير باستخدام LINQ
            return professor.Courses.Select(course => new WorkloadReportDto
            {
                CourseName = course.Name,
                CourseCode = course.Code,
                WeeklyHours = schedules?
                    .Where(s => s.CourseId == course.Id)
                    .Sum(s => s.DurationHours) ?? 0,
                Semester = course.SemesterName
            }).ToList();
        }
    }
}
