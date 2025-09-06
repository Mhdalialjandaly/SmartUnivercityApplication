using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Enums;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class ReportServices : Injectable, IReportServices
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Enrollment> _enrollmentRepository;
        private readonly IRepository<Attendance> _attendanceRepository;
        private readonly IRepository<CourseEvaluation> _evaluationRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Exam> _examRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ReportServices(
            IRepository<Student> studentRepository,
            IRepository<Course> courseRepository,
            IRepository<Enrollment> enrollmentRepository,
            IRepository<Attendance> attendanceRepository,
            IRepository<CourseEvaluation> evaluationRepository,
            IRepository<Department> departmentRepository,
            IRepository<Exam> examRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _attendanceRepository = attendanceRepository;
            _evaluationRepository = evaluationRepository;
            _departmentRepository = departmentRepository;
            _examRepository = examRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReportSummaryDto> GetReportSummaryAsync()
        {
            try
            {
                var students = await _studentRepository.GetAllAsync();
                var enrollments = await _enrollmentRepository.GetAllAsync();
                var attendances = await _attendanceRepository.GetAllAsync();
                var evaluations = await _evaluationRepository.GetAllAsync();

                var totalStudents = students.Count();
                var activeStudents = students.Count(s => s.Status == StudentStatus.Active);

                // حساب متوسط الحضور (آخر 3 أشهر)
                var threeMonthsAgo = DateTime.Now.AddMonths(-3);
                var recentAttendances = attendances.Where(a => a.Date >= threeMonthsAgo);
                var attendanceRate = recentAttendances.Any()
                    ? (recentAttendances.Count(a => a.IsPresent) * 100.0 / recentAttendances.Count())
                    : 0;

                // حساب متوسط التقييم
                var averageRating = evaluations.Any()
                    ? evaluations.Average(e => e.Rating)
                    : 0;

                // حساب نسبة النجاح (درجات أكثر من 50)
                var gradedEnrollments = enrollments.Where(e => e.FinalGrade.HasValue);
                var successRate = gradedEnrollments.Any()
                    ? (gradedEnrollments.Count(e => e.FinalGrade >= 50) * 100.0 / gradedEnrollments.Count())
                    : 0;

                return new ReportSummaryDto
                {
                    TotalStudents = totalStudents,
                    ActiveStudents = activeStudents,
                    AttendanceRate = Math.Round(attendanceRate, 1),
                    AverageRating = Math.Round(averageRating, 1),
                    SuccessRate = Math.Round(successRate, 1)
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("حدث خطأ أثناء جلب ملخص التقرير", ex);
            }
        }

        public async Task<List<CoursePerformanceDto>> GetCoursePerformanceAsync()
        {
            try
            {
                var courses = await _courseRepository.GetAllAsync(c => c.Enrollments);
                var coursePerformance = courses
                    .Select(c => new CoursePerformanceDto
                    {
                        CourseId = c.Id,
                        CourseName = c.Name,
                        CourseCode = c.Code,
                        AverageScore = c.Enrollments.Where(e => e.FinalGrade.HasValue)
                            .Select(e => e.FinalGrade.Value)
                            .DefaultIfEmpty(0)
                            .Average(),
                        StudentCount = c.Enrollments.Count
                    })
                    .OrderByDescending(c => c.AverageScore)
                    .Take(10)
                    .ToList();

                return coursePerformance.Select(c => new CoursePerformanceDto
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseCode = c.CourseCode,
                    AverageScore = Math.Round(c.AverageScore, 1),
                    StudentCount = c.StudentCount
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("حدث خطأ أثناء جلب أداء المقررات", ex);
            }
        }

        public async Task<List<CourseDetailDto>> GetCourseDetailsAsync()
        {
            try
            {
                var courses = await _courseRepository.GetAllAsync(
                    c => c.Enrollments,
                    c => c.CourseEvaluations,
                    c => c.Department);

                var courseDetails = courses
                    .Select(c => new CourseDetailDto
                    {
                        CourseId = c.Id,
                        CourseName = c.Name,
                        CourseCode = c.Code,
                        DepartmentName = c.Department?.Name ?? "غير محدد",
                        StudentCount = c.Enrollments.Count,
                        AverageScore = c.Enrollments
                            .Where(e => e.FinalGrade.HasValue)
                            .Select(e => e.FinalGrade.Value)
                            .DefaultIfEmpty(0)
                            .Average(),
                        AttendanceRate = CalculateCourseAttendanceRate(c),
                        SuccessRate = CalculateCourseSuccessRate(c),
                        AverageRating = c.CourseEvaluations
                            .Select(e => e.Rating)
                            .DefaultIfEmpty(0)
                            .Average()
                    })
                    .ToList();

                return courseDetails.Select(c => new CourseDetailDto
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseCode = c.CourseCode,
                    DepartmentName = c.DepartmentName,
                    StudentCount = c.StudentCount,
                    AverageScore = Math.Round(c.AverageScore, 1),
                    AttendanceRate = Math.Round(c.AttendanceRate, 1),
                    SuccessRate = Math.Round(c.SuccessRate, 1),
                    AverageRating = Math.Round(c.AverageRating, 1)
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("حدث خطأ أثناء جلب تفاصيل المقررات", ex);
            }
        }

        public async Task<List<AttendanceReportDto>> GetAttendanceReportsAsync()
        {
            try
            {
                var startDate = DateTime.Now.AddMonths(-1);
                var endDate = DateTime.Now;

                var courses = await _courseRepository.GetAllAsync(
                    c => c.Enrollments,
                    c => c.Enrollments.Select(e => e.Attendances));

                var attendanceReports = courses
                    .Select(c => new AttendanceReportDto
                    {
                        CourseId = c.Id,
                        CourseName = c.Name,
                        TotalLectures = c.Enrollments
                            .SelectMany(e => e.Attendances)
                            .Where(a => a.Date >= startDate && a.Date <= endDate)
                            .Count(),
                        PresentStudents = c.Enrollments
                            .SelectMany(e => e.Attendances)
                            .Where(a => a.Date >= startDate && a.Date <= endDate && a.IsPresent)
                            .Count(),
                        AbsentStudents = c.Enrollments
                            .SelectMany(e => e.Attendances)
                            .Where(a => a.Date >= startDate && a.Date <= endDate && !a.IsPresent)
                            .Count(),
                        AttendanceRate = CalculateCourseAttendanceRateForPeriod(c, startDate, endDate)
                    })
                    .Where(a => a.TotalLectures > 0)
                    .ToList();

                return attendanceReports.Select(a => new AttendanceReportDto
                {
                    CourseId = a.CourseId,
                    CourseName = a.CourseName,
                    TotalLectures = a.TotalLectures,
                    PresentStudents = a.PresentStudents,
                    AbsentStudents = a.AbsentStudents,
                    AttendanceRate = Math.Round(a.AttendanceRate, 1)
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("حدث خطأ أثناء جلب تقارير الحضور", ex);
            }
        }

        public async Task<List<StudentPerformanceDto>> GetTopPerformingStudentsAsync(int count = 10)
        {
            try
            {
                var students = await _studentRepository.GetAllAsync(
                    s => s.Enrollments,
                    s => s.Enrollments.Select(e => e.Course));

                var topStudents = students
                    .Where(s => s.Enrollments.Any(e => e.FinalGrade.HasValue))
                    .Select(s => new StudentPerformanceDto
                    {
                        StudentId = s.Id,
                        StudentName = $"{s.FirstName} {s.LastName}",
                        StudentIdNumber = s.StudentId,
                        GPA = s.Enrollments
                            .Where(e => e.FinalGrade.HasValue)
                            .Select(e => e.FinalGrade.Value)
                            .DefaultIfEmpty(0)
                            .Average(),
                        TotalCredits = s.Enrollments
                            .Where(e => e.FinalGrade.HasValue && e.FinalGrade >= 50)
                            .Sum(e => e.Course?.Credits ?? 0),
                        CoursesCount = s.Enrollments
                            .Where(e => e.FinalGrade.HasValue)
                            .Count()
                    })
                    .OrderByDescending(s => s.GPA)
                    .Take(count)
                    .ToList();

                return topStudents.Select(s => new StudentPerformanceDto
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    StudentIdNumber = s.StudentIdNumber,
                    GPA = Math.Round(s.GPA, 2),
                    TotalCredits = s.TotalCredits,
                    CoursesCount = s.CoursesCount
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("حدث خطأ أثناء جلب أفضل الطلاب أداءً", ex);
            }
        }

        public async Task<List<DepartmentPerformanceDto>> GetDepartmentPerformanceAsync()
        {
            try
            {
                var departments = await _departmentRepository.GetAllAsync(
                    d => d.Students,
                    d => d.Courses,
                    d => d.Courses.Select(c => c.Enrollments));

                var departmentPerformance = departments
                    .Select(d => new DepartmentPerformanceDto
                    {
                        DepartmentId = d.Id,
                        DepartmentName = d.Name,
                        TotalStudents = d.Students.Count,
                        AverageGPA = d.Students
                            .SelectMany(s => s.Enrollments)
                            .Where(e => e.FinalGrade.HasValue)
                            .Select(e => e.FinalGrade.Value)
                            .DefaultIfEmpty(0)
                            .Average(),
                        CoursesCount = d.Courses.Count,
                        SuccessRate = d.Students
                            .SelectMany(s => s.Enrollments)
                            .Where(e => e.FinalGrade.HasValue)
                            .Select(e => e.FinalGrade >= 50 ? 1.0 : 0.0)
                            .DefaultIfEmpty(0)
                            .Average() * 100
                    })
                    .ToList();

                return departmentPerformance.Select(d => new DepartmentPerformanceDto
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    TotalStudents = d.TotalStudents,
                    AverageGPA = Math.Round(d.AverageGPA, 2),
                    CoursesCount = d.CoursesCount,
                    SuccessRate = Math.Round(d.SuccessRate, 1)
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("حدث خطأ أثناء جلب أداء الأقسام", ex);
            }
        }

        public async Task<List<ExamPerformanceDto>> GetExamPerformanceAsync()
        {
            try
            {
                var exams = await _examRepository.GetAllAsync(
                    e => e.Course,
                    e => e.Enrollments);

                var examPerformance = exams
                    .Select(e => new ExamPerformanceDto
                    {
                        ExamId = e.Id,
                        ExamName = e.Title,
                        CourseName = e.Course?.Name ?? "غير محدد",
                        AverageScore = e.Enrollments
                            .Where(en => en.ExamScore.HasValue)
                            .Select(en => en.ExamScore.Value)
                            .DefaultIfEmpty(0)
                            .Average(),
                        PassRate = e.Enrollments
                            .Where(en => en.ExamScore.HasValue)
                            .Select(en => en.ExamScore >= (e.Course?.PassMark ?? 50) ? 1.0 : 0.0)
                            .DefaultIfEmpty(0)
                            .Average() * 100,
                        HighestScore = e.Enrollments
                            .Where(en => en.ExamScore.HasValue)
                            .Select(en => en.ExamScore.Value)
                            .DefaultIfEmpty(0)
                            .Max(),
                        LowestScore = e.Enrollments
                            .Where(en => en.ExamScore.HasValue)
                            .Select(en => en.ExamScore.Value)
                            .DefaultIfEmpty(0)
                            .Min()
                    })
                    .ToList();

                return examPerformance.Select(e => new ExamPerformanceDto
                {
                    ExamId = e.ExamId,
                    ExamName = e.ExamName,
                    CourseName = e.CourseName,
                    AverageScore = Math.Round(e.AverageScore, 1),
                    PassRate = Math.Round(e.PassRate, 1),
                    HighestScore = e.HighestScore,
                    LowestScore = e.LowestScore
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("حدث خطأ أثناء جلب أداء الامتحانات", ex);
            }
        }

        public async Task<List<SemesterReportDto>> GetSemesterReportsAsync(int academicYear, int semester)
        {
            try
            {
                var enrollments = await _enrollmentRepository.GetAllAsync(
                    e => e.Course,
                    e => e.Student);

                var filteredEnrollments = enrollments
                    .Where(e => e.Course != null &&
                               e.Course.AcademicYear == academicYear &&
                               e.Course.Semester == semester);

                var semesterReports = new List<SemesterReportDto>
                {
                    new SemesterReportDto
                    {
                        SemesterId = semester,
                        SemesterName = $"{semester}-{academicYear}",
                        TotalStudents = filteredEnrollments
                            .Select(e => e.StudentId)
                            .Distinct()
                            .Count(),
                        TotalCourses = filteredEnrollments
                            .Select(e => e.CourseId)
                            .Distinct()
                            .Count(),
                        AverageGPA = filteredEnrollments
                            .Where(e => e.FinalGrade.HasValue)
                            .Select(e => e.FinalGrade.Value)
                            .DefaultIfEmpty(0)
                            .Average(),
                        PassRate = filteredEnrollments
                            .Where(e => e.FinalGrade.HasValue)
                            .Select(e => e.FinalGrade >= 50 ? 1.0 : 0.0)
                            .DefaultIfEmpty(0)
                            .Average() * 100
                    }
                };

                return semesterReports.Select(s => new SemesterReportDto
                {
                    SemesterId = s.SemesterId,
                    SemesterName = s.SemesterName,
                    TotalStudents = s.TotalStudents,
                    TotalCourses = s.TotalCourses,
                    AverageGPA = Math.Round(s.AverageGPA, 2),
                    PassRate = Math.Round(s.PassRate, 1)
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("حدث خطأ أثناء جلب تقارير الفصل الدراسي", ex);
            }
        }

        public async Task<ReportSummaryDto> GetReportSummaryBySemesterAsync(int academicYear, int semester)
        {
            try
            {
                var enrollments = await _enrollmentRepository.GetAllAsync(
                    e => e.Course,
                    e => e.Student);

                var filteredEnrollments = enrollments
                    .Where(e => e.Course != null &&
                               e.Course.AcademicYear == academicYear &&
                               e.Course.Semester == semester);

                var totalStudents = filteredEnrollments
                    .Select(e => e.StudentId)
                    .Distinct()
                    .Count();

                // حساب معدل الحضور للفصل المحدد
                var startDate = semester == 1 ? new DateTime(academicYear, 9, 1) : new DateTime(academicYear, 2, 1);
                var endDate = semester == 1 ? new DateTime(academicYear, 12, 31) : new DateTime(academicYear, 6, 30);

                var attendances = await _attendanceRepository.GetAllAsync();
                var semesterAttendances = attendances
                    .Where(a => a.Date >= startDate && a.Date <= endDate);

                var attendanceRate = semesterAttendances.Any()
                    ? (semesterAttendances.Count(a => a.IsPresent) * 100.0 / semesterAttendances.Count())
                    : 0;

                // حساب متوسط التقييم للفصل المحدد
                var evaluations = await _evaluationRepository.GetAllAsync(e => e.Course);
                var semesterEvaluations = evaluations
                    .Where(e => e.Course != null &&
                               e.Course.AcademicYear == academicYear &&
                               e.Course.Semester == semester);

                var averageRating = semesterEvaluations.Any()
                    ? semesterEvaluations.Average(e => e.Rating)
                    : 0;

                // حساب نسبة النجاح للفصل المحدد
                var successRate = filteredEnrollments
                    .Where(e => e.FinalGrade.HasValue)
                    .Select(e => e.FinalGrade >= 50 ? 1.0 : 0.0)
                    .DefaultIfEmpty(0)
                    .Average() * 100;

                return new ReportSummaryDto
                {
                    TotalStudents = totalStudents,
                    AttendanceRate = Math.Round(attendanceRate, 1),
                    AverageRating = Math.Round(averageRating, 1),
                    SuccessRate = Math.Round(successRate, 1)
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("حدث خطأ أثناء جلب ملخص التقرير للفصل الدراسي", ex);
            }
        }

        // طرق مساعدة خاصة
        private double CalculateCourseAttendanceRate(Course course)
        {
            var threeMonthsAgo = DateTime.Now.AddMonths(-3);
            var totalAttendances = course.Enrollments
                .SelectMany(e => e.Attendances)
                .Where(a => a.Date >= threeMonthsAgo)
                .Count();

            if (totalAttendances == 0) return 0;

            var presentAttendances = course.Enrollments
                .SelectMany(e => e.Attendances)
                .Where(a => a.Date >= threeMonthsAgo && a.IsPresent)
                .Count();

            return (presentAttendances * 100.0) / totalAttendances;
        }

        private double CalculateCourseSuccessRate(Course course)
        {
            var gradedEnrollments = course.Enrollments.Where(e => e.FinalGrade.HasValue);
            if (!gradedEnrollments.Any()) return 0;

            var successfulEnrollments = gradedEnrollments.Count(e => e.FinalGrade >= 50);
            return (successfulEnrollments * 100.0) / gradedEnrollments.Count();
        }

        private double CalculateCourseAttendanceRateForPeriod(Course course, DateTime startDate, DateTime endDate)
        {
            var totalAttendances = course.Enrollments
                .SelectMany(e => e.Attendances)
                .Where(a => a.Date >= startDate && a.Date <= endDate)
                .Count();

            if (totalAttendances == 0) return 0;

            var presentAttendances = course.Enrollments
                .SelectMany(e => e.Attendances)
                .Where(a => a.Date >= startDate && a.Date <= endDate && a.IsPresent)
                .Count();

            return (presentAttendances * 100.0) / totalAttendances;
        }

        public Task<List<StudentPerformanceDto>> GetStudentsByGPARangeAsync(double minGPA, double maxGPA)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExamPerformanceDto>> GetTopPerformingExamsAsync(int count = 5)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseDetailDto>> GetCoursesByDepartmentAsync(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<ReportDto> CreateReportAsync(ReportDto dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReportAsync(int id, ReportDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteReportAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}