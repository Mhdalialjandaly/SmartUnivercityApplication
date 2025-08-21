using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Enums;
using UniversityManagementSystem.Domain.Interfaces;
using System.Linq.Expressions;
using UniversityManagementSystem.Application.Models;

namespace UniversityManagementSystem.Application.Services
{
    public class StudentAttendanceServices : Injectable, IStudentAttendanceServices
    {
        private readonly IRepository<StudentAttendance> _attendanceRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StudentAttendanceServices(
            IRepository<StudentAttendance> attendanceRepository,
            IRepository<Student> studentRepository,
            IRepository<Course> courseRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _attendanceRepository = attendanceRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StudentAttendanceDto>> GetAllAttendancesAsync()
        {
            var attendances = await _attendanceRepository.GetAllAsync(e => e.Student, x => x.Course);
            return _mapper.Map<List<StudentAttendanceDto>>(attendances);
        }

        public async Task<StudentAttendanceDto> GetAttendanceByIdAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id, e => e.Student, x => x.Course);
            return attendance != null ? _mapper.Map<StudentAttendanceDto>(attendance) : null;
        }

        public async Task<StudentAttendanceDto> CreateAttendanceAsync(StudentAttendanceDto attendanceDto)
        {
            // التحقق من وجود الطالب والمادة
            var student = await _studentRepository.GetByIdAsync(attendanceDto.StudentId);
            if (student == null)
                throw new Exception($"Student with ID {attendanceDto.StudentId} not found");

            var course = await _courseRepository.GetByIdAsync(attendanceDto.CourseId);
            if (course == null)
                throw new Exception($"Course with ID {attendanceDto.CourseId} not found");

            var attendance = _mapper.Map<StudentAttendance>(attendanceDto);
            await _attendanceRepository.AddAsync(attendance);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<StudentAttendanceDto>(attendance);
        }

        public async Task UpdateAttendanceAsync(int id, StudentAttendanceDto attendanceDto)
        {
            var existingAttendance = await _attendanceRepository.GetByIdAsync(id);
            if (existingAttendance == null)
                throw new Exception($"Attendance record with ID {id} not found");

            // التحقق من وجود الطالب والمادة إذا تم تحديثها
            if (existingAttendance.StudentId != attendanceDto.StudentId)
            {
                var student = await _studentRepository.GetByIdAsync(attendanceDto.StudentId);
                if (student == null)
                    throw new Exception($"Student with ID {attendanceDto.StudentId} not found");
            }

            if (existingAttendance.CourseId != attendanceDto.CourseId)
            {
                var course = await _courseRepository.GetByIdAsync(attendanceDto.CourseId);
                if (course == null)
                    throw new Exception($"Course with ID {attendanceDto.CourseId} not found");
            }

            _mapper.Map(attendanceDto, existingAttendance);
            _attendanceRepository.Update(existingAttendance);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAttendanceAsync(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
                throw new Exception($"Attendance record with ID {id} not found");

            _attendanceRepository.Delete(attendance);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> AttendanceExistsAsync(int id)
        {
            return await _attendanceRepository.ExistsAsync(a => a.Id == id);
        }

        public async Task<List<StudentAttendanceDto>> GetAttendancesByStudentAsync(int studentId)
        {
            var attendances = await _attendanceRepository.GetAllAsync(
                a => a.StudentId == studentId, e => e.Student, x => x.Course);

            return _mapper.Map<List<StudentAttendanceDto>>(attendances);
        }

        public async Task<List<StudentAttendanceDto>> GetAttendancesByCourseAsync(int courseId)
        {
            var attendances = await _attendanceRepository.GetAllAsync(
                a => a.CourseId == courseId, e => e.Student, x => x.Course);

            return _mapper.Map<List<StudentAttendanceDto>>(attendances);
        }

        public async Task<List<StudentAttendanceDto>> GetAttendancesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var attendances = await _attendanceRepository.GetAllAsync(
                a => a.AttendanceDate >= startDate && a.AttendanceDate <= endDate,
                e => e.Student, x => x.Course);

            return _mapper.Map<List<StudentAttendanceDto>>(attendances);
        }

        public async Task<StudentsAttendanceSummaryDto> GetAttendanceSummaryAsync(int studentId, int courseId)
        {
            // التحقق من وجود الطالب والمادة
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                throw new Exception($"Student with ID {studentId} not found");

            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
                throw new Exception($"Course with ID {courseId} not found");

            var attendances = await _attendanceRepository.GetAllAsync(
                a => a.StudentId == studentId && a.CourseId == courseId);

            var totalSessions = attendances.Count();
            var presentCount = attendances.Count(a => a.Status == AttendanceStatus.Present);
            var absentCount = attendances.Count(a => a.Status == AttendanceStatus.Absent);
            var excusedCount = attendances.Count(a => a.Status == AttendanceStatus.Excused);
            var lateCount = attendances.Count(a => a.Status == AttendanceStatus.Late);

            return new StudentsAttendanceSummaryDto
            {
                StudentId = studentId,
                CourseId = courseId,
                PresentCount = presentCount,
                AbsentCount = absentCount,
                ExcusedCount = excusedCount,
                LateCount = lateCount,
                TotalSessions = totalSessions
            };
        }

        // ملخص عام للحضور
        public async Task<StudentsAttendanceSummaryDto> GetAttendanceSummaryAsync()
        {
            var attendances = await _attendanceRepository.GetAllAsync();

            var totalSessions = attendances.Count();
            var presentCount = attendances.Count(a => a.Status == AttendanceStatus.Present);
            var absentCount = attendances.Count(a => a.Status == AttendanceStatus.Absent);
            var excusedCount = attendances.Count(a => a.Status == AttendanceStatus.Excused);
            var lateCount = attendances.Count(a => a.Status == AttendanceStatus.Late);

            return new StudentsAttendanceSummaryDto
            {
                PresentCount = presentCount,
                AbsentCount = absentCount,
                ExcusedCount = excusedCount,
                LateCount = lateCount,
                TotalSessions = totalSessions
            };
        }

        // الحصول على الحضور مع دعم الترقيم والتصفية
        public async Task<PaginatedResult<StudentAttendanceDto>> GetAttendancesPagedAsync(
            int page,
            int pageSize,
            string searchTerm,
            int? courseId,
            int? departmentId,
            DateTime? date,
            AttendanceStatus status)
        {
            // بناء تعبير التصفية الديناميكي
            var predicate = PredicateBuilder.True<StudentAttendance>();

            // تطبيق فلتر البحث
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTermLower = searchTerm.ToLower();
                predicate = predicate.And(a =>
                    a.Student.FirstName.ToLower().Contains(searchTermLower) ||
                    a.Student.LastName.ToLower().Contains(searchTermLower) ||
                    a.Student.StudentId.ToLower().Contains(searchTermLower) ||
                    a.Course.Name.ToLower().Contains(searchTermLower));
            }

            // تطبيق فلتر المادة
            if (courseId.HasValue && courseId.Value > 0)
            {
                predicate = predicate.And(a => a.CourseId == courseId.Value);
            }

            // تطبيق فلتر القسم (إذا كان متوفرًا في نموذج البيانات)
            // if (departmentId.HasValue && departmentId.Value > 0)
            // {
            //     predicate = predicate.And(a => a.Student.DepartmentId == departmentId.Value);
            // }

            // تطبيق فلتر التاريخ
            if (date.HasValue)
            {
                var dateOnly = date.Value.Date;
                predicate = predicate.And(a => a.AttendanceDate.Date == dateOnly);
            }

            // تطبيق فلتر الحالة
            if (status != AttendanceStatus.All) // افترض أن لديك قيمة All في التعداد
            {
                predicate = predicate.And(a => a.Status == status);
            }

            // الحصول على البيانات مع العلاقات
            var attendances = await _attendanceRepository.FindAsync(
                predicate,
                e => e.Student,
                x => x.Course);

            // تطبيق الترقيم
            var totalRecords = attendances.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var pagedData = attendances
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedResult<StudentAttendanceDto>
            {
                Data = _mapper.Map<List<StudentAttendanceDto>>(pagedData),
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                CurrentPage = page
            };
        }

        public async Task<List<StudentsAttendanceReportDto>> GenerateAttendanceReportAsync(int courseId, DateTime startDate, DateTime endDate)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
                throw new Exception($"Course with ID {courseId} not found");

            var students = await _studentRepository.GetAllAsync();
            var report = new List<StudentsAttendanceReportDto>();

            foreach (var student in students)
            {
                var attendances = await _attendanceRepository.GetAllAsync(
                    a => a.StudentId == student.Id &&
                         a.CourseId == courseId &&
                         a.AttendanceDate >= startDate &&
                         a.AttendanceDate <= endDate);

                var presentCount = attendances.Count(a => a.Status == AttendanceStatus.Present);
                var totalSessions = attendances.Count();

                report.Add(new StudentsAttendanceReportDto
                {
                    StudentId = student.Id,
                    StudentName = $"{student.FirstName} {student.LastName}",
                    StudentCode = student.StudentId,
                    CourseId = courseId,
                    CourseName = course.Name,
                    TotalSessions = totalSessions,
                    PresentCount = presentCount,
                    AttendancePercentage = totalSessions > 0 ? (presentCount * 100.0 / totalSessions) : 0,
                    Status = totalSessions > 0 ?
                        (presentCount * 100.0 / totalSessions >= 75 ? "Good" : "Warning") : "No Data"
                });
            }

            return report.OrderByDescending(r => r.AttendancePercentage).ToList();
        }
    }

    // مساعد لبناء التعبيرات الديناميكية
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}