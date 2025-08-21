using AutoMapper;
using System.Linq;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class CourseServices : Injectable, ICourseServices
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<CourseRegistration> _registrationRepository;

        public CourseServices(
            IRepository<Course> courseRepository,
            IRepository<Department> departmentRepository,
            IRepository<CourseRegistration> registrationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
            _registrationRepository = registrationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return _mapper.Map<List<CourseDto>>(courses);
        }

        public async Task<List<CourseDto>> GetCoursesByDepartmentAsync(int departmentId)
        {
            var courses = await _courseRepository.GetAllAsync(c => c.DepartmentId == departmentId);
            return _mapper.Map<List<CourseDto>>(courses);
        }

        public async Task<List<CourseDto>> GetActiveCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync(c => c.IsActive);
            return _mapper.Map<List<CourseDto>>(courses);
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return course != null ? _mapper.Map<CourseDto>(course) : null;
        }

        public async Task<CourseDto> CreateCourseAsync(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            await _courseRepository.AddAsync(course);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CourseDto>(course);
        }

        public async Task UpdateCourseAsync(int id, CourseDto courseDto)
        {
            var existingCourse = await _courseRepository.GetByIdAsync(id);
            if (existingCourse == null)
                throw new Exception($"Course with ID {id} not found");

            _mapper.Map(courseDto, existingCourse);
            _courseRepository.Update(existingCourse);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
                throw new Exception($"Course with ID {id} not found");

            _courseRepository.Delete(course);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> CourseExistsAsync(int id)
        {
            return await _courseRepository.ExistsAsync(c => c.Id == id);
        }

        public async Task<CourseDto?> GetCourseByCodeAsync(string code)
        {
            var course = await _courseRepository.FindAsync(c => c.Code == code);
            return course != null ? _mapper.Map<CourseDto>(course) : null;
        }

        public async Task<int> GetCoursesCountAsync()
        {
            return await _courseRepository.CountAsync();
        }

        public async Task<int> GetActiveCoursesCountAsync()
        {
            return await _courseRepository.CountAsync(c => c.IsActive);
        }

        public async Task<List<CourseDto>> GetCoursesByInstructorAsync(string instructorName)
        {
            var courses = await _courseRepository.GetAllAsync(c => c.Instructor.Contains(instructorName));
            return _mapper.Map<List<CourseDto>>(courses);
        }

        public async Task<decimal> GetCourseFeeAsync(int courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            return course?.Fee ?? 0;
        }

        public async Task UpdateCourseFeeAsync(int courseId, decimal newFee)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course != null)
            {
                course.Fee = newFee;
                _courseRepository.Update(course);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<List<CourseDto>> SearchCoursesAsync(string searchTerm)
        {
            var courses = await _courseRepository.GetAllAsync(c =>
                c.Name.Contains(searchTerm) ||
                c.Code.Contains(searchTerm) ||
                c.Description.Contains(searchTerm));
            return _mapper.Map<List<CourseDto>>(courses);
        }

     

        public async Task<CourseSearchResult> GetCoursesAsync(int pageNumber, int pageSize, string searchTerm, int? departmentId, bool? isActive)
        {
            var query = await _courseRepository.GetPagedAsync(pageNumber,pageSize);           

            var courseDtos = _mapper.Map<List<CourseDto>>(query);
           

            return new CourseSearchResult { Courses = courseDtos ,TotalCount = courseDtos.Count , TotalPages = 5};
        }

        public async Task<CourseStatisticsDto> GetCourseStatisticsAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            var registrations = await _registrationRepository.GetAllAsync();

            return new CourseStatisticsDto
            {
                TotalCourses = courses.Count(),
                ActiveCourses = courses.Count(c => c.IsActive),
                InactiveCourses = courses.Count(c => !c.IsActive),
                TotalRevenue = registrations.Where(r => r.Status == "مسجل" || r.Status == "مكتمل").Sum(r => r.AmountPaid),
                TotalStudents = registrations.Where(r => r.Status == "مسجل" || r.Status == "مكتمل").Select(r => r.StudentId).Distinct().Count(),
                AverageCredits = courses.Any() ? courses.Average(c => c.Credits) : 0
            };
        }

        public async Task<List<CourseDto>> GetPopularCoursesAsync(int count)
        {
            var courses = await _courseRepository.GetAllAsync(c => c.Department);
            var registrations = await _registrationRepository.GetAllAsync();

            var popularCourses = courses
                .Select(c => new { Course = c, RegistrationCount = registrations.Count(r => r.CourseId == c.Id) })
                .OrderByDescending(x => x.RegistrationCount)
                .Take(count)
                .Select(x => x.Course);

            return _mapper.Map<List<CourseDto>>(popularCourses);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            var registrations = await _registrationRepository.GetAllAsync();
            return registrations.Where(r => r.Status == "مسجل" || r.Status == "مكتمل").Sum(r => r.AmountPaid);
        }

        public async Task<List<CourseDto>> GetCoursesBySemesterAsync(int semester, int academicYear)
        {
            var courses = await _courseRepository.GetAllAsync(
                c => c.Semester == semester && c.AcademicYear == academicYear,
                c => c.Department);
            return _mapper.Map<List<CourseDto>>(courses);
        }

        public async Task<bool> IsCourseFullAsync(int courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            return course != null && course.CurrentStudents >= course.MaxStudents;
        }

        public async Task<int> GetAvailableSeatsAsync(int courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            return course != null ? Math.Max(0, course.MaxStudents - course.CurrentStudents) : 0;
        }

        public async Task<List<CourseDto>> GetPrerequisiteCoursesAsync(string prerequisites)
        {
            if (string.IsNullOrEmpty(prerequisites))
                return new List<CourseDto>();

            var prerequisiteCodes = prerequisites.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(code => code.Trim())
                .ToList();

            var courses = await _courseRepository.GetAllAsync(
                c => prerequisiteCodes.Contains(c.Code),
                c => c.Department);

            return _mapper.Map<List<CourseDto>>(courses);
        }
        public async Task<List<Course>> GetTopEnrolledCoursesAsync(
            int topCount = 5,
            string academicYear = null,
            int? departmentId = null)
        {
            try
            {
                // بناء الاستعلام الأساسي
                var query = await _courseRepository.GetAllAsync();
                query = query.Select(c => new Course
                   {
                        Id = c.Id,
                        Code = c.Code,
                        Name = c.Name,
                        Department = c.Department,
                        EnrolledStudentsCount = c.CourseRegistrations.Count,
                        Capacity = c.Capacity
                    });

                // تطبيق الفلترات الاختيارية
                if (!string.IsNullOrEmpty(academicYear))
                {
                    query = query.Where(c => c.CourseRegistrations
                        .Any(r => r.Student.AcademicYear == academicYear));
                }

                if (departmentId.HasValue)
                {
                    query = query.Where(c => c.Department.Id == departmentId.Value);
                }

                // تنفيذ الاستعلام والحصول على النتائج
                var result =   query
                    .OrderByDescending(c => c.EnrolledStudentsCount)
                    .Take(topCount)
                    .ToList();

                // حساب النسب المئوية للتسجيل
                foreach (var course in result)
                {
                    course.EnrollmentPercentage = course.Capacity > 0
                        ? Math.Round((double)course.EnrolledStudentsCount / course.Capacity * 100, 2)
                        : 0;
                }

                return result;
            }
            catch (Exception ex)
            {
                // يمكنك استخدام نظام التسجيل (Logging) هنا
                throw new ApplicationException("An error occurred while retrieving top enrolled courses.", ex);
            }
        }
        public Task<decimal> GetTotalCourseRevenueAsync(int semester, int year)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseRevenueDto>> GetTopRevenueCoursesAsync(int count = 10)
        {
            throw new NotImplementedException();
        }
    }
}

