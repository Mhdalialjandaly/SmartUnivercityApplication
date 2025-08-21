using AutoMapper;
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
    public class StudentServices : Injectable, IStudentServices 
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<User> _user;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public StudentServices(
            IRepository<Student> studentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentDto> GetStudentByIdAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<List<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<StudentDto> CreateStudentAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            await _studentRepository.AddAsync(student);
            await _unitOfWork.CommitAsync();
            
            return studentDto;
        }

        public async Task UpdateStudentAsync(int studentId, StudentDto studentDto)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(studentId);
            if (existingStudent == null)
                throw new Exception("Student not found");

            _mapper.Map(studentDto, existingStudent);
            _studentRepository.Update(existingStudent);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                throw new Exception("Student not found");

            _studentRepository.Delete(student);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> StudentExistsAsync(int studentId)
        {
            return await _studentRepository.ExistsAsync(s => s.Id == studentId);
        }

        public async Task<decimal> GetStudentAccountBalanceAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                throw new Exception("Student not found");

            return student.AccountBalance;
        }

        public async Task<bool> UpdateStudentAccountBalanceAsync(int studentId, decimal amount)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                return false;

            student.AccountBalance += amount;
            _studentRepository.Update(student);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> ChangeStudentStatusAsync(int studentId, StudentStatus status)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                return false;

            student.Status = status;
            _studentRepository.Update(student);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<List<CourseRegistrationDto>> GetStudentCoursesAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId, s => s.CourseRegistrations);
            return _mapper.Map<List<CourseRegistrationDto>>(student.CourseRegistrations).ToList() ?? new List<CourseRegistrationDto>();
        }

        public async Task<List<StudentDocumentDto>> GetStudentDocumentsAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId, s => s.StudentDocuments);
            return _mapper.Map<List<StudentDocumentDto>>(student.CourseRegistrations).ToList() ?? new List<StudentDocumentDto>();
        }

        public async Task<bool> CompleteRegistrationAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                return false;

            student.RegistraionCompleted = true;
            student.RegistrationDate = DateTime.Now;
            _studentRepository.Update(student);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<int> GetTotalStudentsCountAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return students.Count();
        }

        public async Task<int> GetActiveStudentsCountAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return students.Where(e=>e.Status == StudentStatus.Active).Count();
        }  
        
        public async Task<int> GetStudentsCountByGenderAsync(bool isMale)
        {
            var students = await _studentRepository.GetAllAsync();
            return students.Where(e=>e.Sexual == isMale).Count();
        }

        public async Task<int> GetNewStudentsCountAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return students.Where(e => e.AcademicYear == "السنة الأولى").Count();
        }

        public async Task<double> GetAverageGPAAsync()
        {
            var students = await _studentRepository.GetAllAsync(e => e.Department.Courses);
            var courses = students.Select(e => e.Department.Courses);
            return courses.Average(e => e.Select(d => d.GPA).Count());
        }

        public async Task<PaginatedResult<Student>> GetStudentsPagedAsync(int pageNumber, int pageSize, string term, int? departmentId, StudentStatus status)
        {
            try
            {
                // 1. بناء الاستعلام الأساسي مع التضمينات اللازمة
                var query =await _studentRepository.GetAllAsync(
                    s => s.Department,
                    s => s.Nationality,
                    s => s.Tunnel
                );
                var res = query.ToList();
                // 2. تطبيق الفلاتر
                if (!string.IsNullOrEmpty(term))
                {
                    query = query.Where(s =>
                        s.FirstName.Contains(term) ||
                        s.LastName.Contains(term) ||
                        s.StudentId.Contains(term));
                }

                if (departmentId.HasValue)
                {
                    query = query.Where(s => s.DepartmentId == departmentId.Value);
                }

                if (status != StudentStatus.All)
                {
                    query = query.Where(s => s.Status == status);
                }

                // 3. الحصول على العدد الكلي قبل التقسيم إلى صفحات
                var totalRecords =  query.Count();

                // 4. تطبيق التقسيم إلى صفحات وجلب البيانات
                var students =  query
                    .OrderBy(s => s.LastName)
                    .ThenBy(s => s.FirstName)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                

                return new PaginatedResult<Student>
                {
                    Data = students,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalRecords = totalRecords,
                    TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
                };
            }
            catch (Exception ex)
            {
                // يمكنك استخدام نظام التسجيل (Logging) هنا
                throw new ApplicationException("حدث خطأ أثناء جلب بيانات الطلاب", ex);
            }
        }
        public async Task<List<InvoiceDto>> GetStudentInvoicesAsync(int studentId)
        {
            List<CourseRegistration> courses = new List<CourseRegistration>();
            try
            {
                var invoices = new List<InvoiceDto>();
                var Students = await _studentRepository.GetByIdAsync(studentId,e => e.CourseRegistrations);

                if (Students.CourseRegistrations is null)
                    courses = new List<CourseRegistration>();
                else
                    courses = Students.CourseRegistrations.ToList();

                foreach (var item in courses)
                {
                    invoices.Add(new InvoiceDto {Amount = item.CourseFee , Date = DateTime.Now,InvoiceNumber = GenerateInvoiceNumber() });
                }
                return invoices;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error retrieving invoices for student {StudentId}", studentId);
                throw new Exception($"An error occurred while fetching invoices for student {studentId}.", ex);
            }
        }
        private string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.Now:yyyy}-{new Random().Next(10000, 99999)}";
        }

        public async Task<int> GetNewStudentsCountAsync(DateTime? startDate = null, DateTime? endDate = null, string academicYear = null)
        {
            try
            {
                // تحديد الفترة الزمنية الافتراضية إذا لم يتم توفيرها
                var effectiveStartDate = startDate ?? DateTime.Now.AddMonths(-1); // آخر شهر كافتراضي
                var effectiveEndDate = endDate ?? DateTime.Now;

                // بناء الاستعلام الأساسي
                var query = await _studentRepository.GetAllAsync();
                query = query.Where(s => s.RegistrationDate >= effectiveStartDate &&
                               s.RegistrationDate <= effectiveEndDate &&
                               s.RegistraionCompleted);

                // تطبيق فلترة السنة الأكاديمية إذا تم توفيرها
                if (!string.IsNullOrEmpty(academicYear))
                {
                    query = query.Where(s => s.AcademicYear == academicYear);
                }

                // تنفيذ الاستعلام وإرجاع النتيجة
                return  query.Count();
            }
            catch (Exception ex)
            {
                // يمكنك استخدام نظام التسجيل (Logging) هنا
                throw new ApplicationException("An error occurred while counting new students.", ex);
            }
        }
        public async Task<List<StudentCountryDistributionDto>> GetStudentsByCountryAsync(
            DateTime? startDate = null,
            DateTime? endDate = null,
            string academicYear = null)
        {
            try
            {
                // تطبيق الفلترات الأساسية
                var query = await _studentRepository.GetAllAsync();
                query = query.Where(s => s.RegistraionCompleted);

                // فلترة حسب التاريخ إذا تم توفيره
                if (startDate.HasValue)
                {
                    query = query.Where(s => s.RegistrationDate >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(s => s.RegistrationDate <= endDate.Value);
                }

                // فلترة حسب السنة الأكاديمية إذا تم توفيرها
                if (!string.IsNullOrEmpty(academicYear))
                {
                    query = query.Where(s => s.AcademicYear == academicYear);
                }

                // تجميع البيانات حسب الدولة
                var result =  query
                    .GroupBy(s => new { s.Nationality.CountryCode, s.Nationality.CountryName })
                    .Select(g => new StudentCountryDistributionDto
                    {
                        CountryCode = g.Key.CountryCode,
                        CountryName = g.Key.CountryName,
                        StudentCount = g.Count(),
                        Percentage = 0 // سيتم حسابها لاحقا
                    })
                    .OrderByDescending(x => x.StudentCount)
                    .ToList();

                // حساب النسب المئوية
                var totalStudents = result.Sum(x => x.StudentCount);
                if (totalStudents > 0)
                {
                    foreach (var item in result)
                    {
                        item.Percentage = Math.Round((double)item.StudentCount / totalStudents * 100, 2);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // يمكنك استخدام نظام التسجيل (Logging) هنا
                throw new ApplicationException("An error occurred while retrieving students by country.", ex);
            }
        }
    }
}
