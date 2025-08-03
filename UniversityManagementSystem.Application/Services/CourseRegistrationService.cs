using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class CourseRegistrationService : Injectable, ICourseRegistrationService
    {
        private readonly IRepository<CourseRegistration> _registrationRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CourseRegistrationService(
            IRepository<CourseRegistration> registrationRepository,
            IRepository<Student> studentRepository,
            IRepository<Course> courseRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _registrationRepository = registrationRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CourseRegistrationDto>> GetAllRegistrationsAsync()
        {
            var registrations = await _registrationRepository.GetAllAsync(r => r.Student, r => r.Course);
            return _mapper.Map<List<CourseRegistrationDto>>(registrations);
        }

        public async Task<List<CourseRegistrationDto>> GetRegistrationsByStudentIdAsync(string studentId)
        {
            var registrations = await _registrationRepository.GetAllAsync(
                r => r.StudentId == studentId,
                r => r.Student,
                r => r.Course);

            return _mapper.Map<List<CourseRegistrationDto>>(registrations);
        }

        public async Task<List<CourseRegistrationDto>> GetRegistrationsByCourseIdAsync(int courseId)
        {
            var registrations = await _registrationRepository.GetAllAsync(
                r => r.CourseId == courseId,
                r => r.Student,
                r => r.Course);

            return _mapper.Map<List<CourseRegistrationDto>>(registrations);
        }

        public async Task<List<CourseRegistrationDto>> GetRegistrationsByStatusAsync(string status)
        {
            var registrations = await _registrationRepository.GetAllAsync(
                r => r.Status == status,
                r => r.Student,
                r => r.Course);

            return _mapper.Map<List<CourseRegistrationDto>>(registrations);
        }

        public async Task<CourseRegistrationDto?> GetRegistrationByIdAsync(int id)
        {
            var registration = await _registrationRepository.GetByIdAsync(id, r => r.Student, r => r.Course);
            return registration != null ? _mapper.Map<CourseRegistrationDto>(registration) : null;
        }

        public async Task<CourseRegistrationDto> CreateRegistrationAsync(CourseRegistrationDto registrationDto)
        {
            var registration = _mapper.Map<CourseRegistration>(registrationDto);

            registration.RegistrationDate = DateTime.Now;
            registration.PaymentDate = DateTime.Now;

            // تحقق من وجود الطالب باستخدام المفتاح الأساسي string
            var student = await _studentRepository.GetByIdAsync(registration.StudentId);
            if (student == null)
                throw new Exception("الطالب غير موجود");

            await _registrationRepository.AddAsync(registration);
            await _unitOfWork.CommitAsync();

            var course = await _courseRepository.GetByIdAsync(registration.CourseId);
            if (course != null)
            {
                course.CurrentStudents++;
                _courseRepository.Update(course);
                await _unitOfWork.CommitAsync();
            }

            return _mapper.Map<CourseRegistrationDto>(registration);
        }

        public async Task UpdateRegistrationAsync(int id, CourseRegistrationDto registrationDto)
        {
            var existingRegistration = await _registrationRepository.GetByIdAsync(id);
            if (existingRegistration == null)
                throw new Exception($"Registration with ID {id} not found");

            _mapper.Map(registrationDto, existingRegistration);
            _registrationRepository.Update(existingRegistration);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRegistrationAsync(int id)
        {
            var registration = await _registrationRepository.GetByIdAsync(id);
            if (registration == null)
                throw new Exception($"Registration with ID {id} not found");

            _registrationRepository.Delete(registration);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> RegistrationExistsAsync(int id)
        {
            return await _registrationRepository.ExistsAsync(r => r.Id == id);
        }

        public async Task<decimal> GetTotalAmountPaidByStudentAsync(string studentId)
        {
            var registrations = await _registrationRepository.GetAllAsync(
                r => r.StudentId == studentId && r.Status == "مسجل");
            return registrations.Sum(r => r.AmountPaid);
        }

        public async Task<int> GetStudentRegistrationCountAsync(string studentId)
        {
            return await _registrationRepository.CountAsync(r => r.StudentId == studentId && r.Status == "مسجل");
        }

        public async Task<int> GetCourseRegistrationCountAsync(int courseId)
        {
            return await _registrationRepository.CountAsync(r => r.CourseId == courseId && r.Status == "مسجل");
        }

        public async Task<List<CourseRegistrationDto>> GetRecentRegistrationsAsync(int count)
        {
            var registrations = await _registrationRepository.GetAllAsync(r => r.Student, r => r.Course);
            var recentRegistrations = registrations.OrderByDescending(r => r.RegistrationDate).Take(count);
            return _mapper.Map<List<CourseRegistrationDto>>(recentRegistrations);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            var registrations = await _registrationRepository.GetAllAsync();
            return registrations.Where(r => r.Status == "مسجل" || r.Status == "مكتمل").Sum(r => r.AmountPaid);
        }

        public async Task<List<CourseRegistrationDto>> GetRegistrationsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var registrations = await _registrationRepository.GetAllAsync(
                r => r.RegistrationDate >= startDate && r.RegistrationDate <= endDate,
                r => r.Student,
                r => r.Course);
            return _mapper.Map<List<CourseRegistrationDto>>(registrations);
        }

        public async Task CancelRegistrationAsync(int id)
        {
            var registration = await _registrationRepository.GetByIdAsync(id);
            if (registration != null)
            {
                registration.Status = "ملغي";
                _registrationRepository.Update(registration);
                await _unitOfWork.CommitAsync();

                // تحديث عدد الطلاب في المادة
                var course = await _courseRepository.GetByIdAsync(registration.CourseId);
                if (course != null)
                {
                    course.CurrentStudents = Math.Max(0, course.CurrentStudents - 1);
                    _courseRepository.Update(course);
                    await _unitOfWork.CommitAsync();
                }
            }
        }

        public async Task CompleteRegistrationAsync(int id)
        {
            var registration = await _registrationRepository.GetByIdAsync(id);
            if (registration != null)
            {
                registration.Status = "مكتمل";
                _registrationRepository.Update(registration);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<bool> IsStudentRegisteredAsync(string studentId, int courseId)
        {
            return await _registrationRepository.ExistsAsync(
                r => r.StudentId == studentId &&
                     r.CourseId == courseId &&
                     (r.Status == "مسجل" || r.Status == "مكتمل"));
        }
    }
}
