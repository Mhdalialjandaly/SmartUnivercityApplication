using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class CourseRegistrationServices : Injectable, ICourseRegistrationServices
    {
        private readonly IRepository<CourseRegistration> _registrationRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CourseRegistrationServices(
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

        public async Task<List<CourseRegistrationDto>> GetRegistrationsByStudentIdAsync(int studentId)
        {
            var registrations = await _registrationRepository.FindAsync(
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

        public async Task<decimal> GetTotalAmountPaidByStudentAsync(int studentId)
        {
            var registrations = await _registrationRepository.GetAllAsync(
                r => r.StudentId == studentId && r.Status == "مسجل");
            return registrations.Sum(r => r.AmountPaid);
        }

        public async Task<int> GetStudentRegistrationCountAsync(int studentId)
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

        public async Task<bool> IsStudentRegisteredAsync(int studentId, int courseId)
        {
            return await _registrationRepository.ExistsAsync(
                r => r.StudentId == studentId &&
                     r.CourseId == courseId &&
                     (r.Status == "مسجل" || r.Status == "مكتمل"));
        }

        public async Task<PaginatedResult<CourseRegistrationDto>> GetRegistrationsPagedAsync(int pageNumber,
            int pageSize,            
            string searchTerm = null,
            int? departmentId = null,
            string status = null)
        {
            // بناء الاستعلام الأساسي مع تضمين العلاقات
            var query =await _registrationRepository.GetAllAsync(r => r.Student,x=>x.Course);

            // تطبيق الفلاتر
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(r =>
                    r.Student.FullName.Contains(searchTerm) ||
                    r.Course.Name.Contains(searchTerm) ||
                    r.Student.StudentId.Contains(searchTerm));
            }

            if (departmentId.HasValue)
            {
                query = query.Where(r => r.Course.DepartmentId == departmentId.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(r => r.Status == status);
            }

            // حساب العدد الإجمالي للسجلات
            var totalCount =  query.Count();

            // تطبيق ترقيم الصفحات
            var registrations =  query
                .OrderByDescending(r => r.RegistrationDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // تحويل النتيجة إلى DTO
            var registrationDtos = _mapper.Map<List<CourseRegistrationDto>>(registrations);

            // إرجاع النتيجة مع معلومات الترقيم
            return new PaginatedResult<CourseRegistrationDto>
            {
                Data = registrationDtos,
                TotalRecords = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<CourseRegistrationSummaryDto> GetRegistrationSummaryAsync()
        {
            // جلب جميع التسجيلات مع العلاقات المطلوبة
            var registrations = await _registrationRepository.GetAllAsync(r => r.Course);

            // حساب الإحصائيات
            var summary = new CourseRegistrationSummaryDto
            {
                TotalRegistrations = registrations.Count(),
                ActiveRegistrations = registrations.Count(r => r.Status == "مسجل"),
                CancelledRegistrations = registrations.Count(r => r.Status == "ملغي"),
                CompletedRegistrations = registrations.Count(r => r.Status == "مكتمل"),
                TotalRevenue = registrations
                    .Where(r => r.Status == "مسجل" || r.Status == "مكتمل")
                    .Sum(r => r.AmountPaid),
                OutstandingPayments = registrations
                    .Where(r => r.Status == "مسجل")
                    .Sum(r => r.Course.Fee - r.AmountPaid),
                AverageAmountPaid = registrations
                    .Where(r => r.Status == "مسجل" || r.Status == "مكتمل")
                    .Average(r => r.AmountPaid),
                MostPopularCourse = registrations
                    .GroupBy(r => r.Course.Name)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault(),
                RecentRegistrationsCount = registrations
                    .Count(r => r.RegistrationDate >= DateTime.Now.AddDays(-7))
            };

            return summary;
        }
    }
}
