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

        public async Task<StudentDto> GetStudentByIdAsync(string studentId)
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
            return _mapper.Map<StudentDto>(student);
        }

        public async Task UpdateStudentAsync(string studentId, StudentDto studentDto)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(studentId);
            if (existingStudent == null)
                throw new Exception("Student not found");

            _mapper.Map(studentDto, existingStudent);
            _studentRepository.Update(existingStudent);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteStudentAsync(string studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                throw new Exception("Student not found");

            _studentRepository.Delete(student);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> StudentExistsAsync(string studentId)
        {
            return await _studentRepository.ExistsAsync(s => s.StudentId == studentId);
        }

        public async Task<decimal> GetStudentAccountBalanceAsync(string studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                throw new Exception("Student not found");

            return student.AccountBalance;
        }

        public async Task<bool> UpdateStudentAccountBalanceAsync(string studentId, decimal amount)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                return false;

            student.AccountBalance += amount;
            _studentRepository.Update(student);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> ChangeStudentStatusAsync(string studentId, StudentStatus status)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                return false;

            student.Status = status;
            _studentRepository.Update(student);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<List<CourseRegistrationDto>> GetStudentCoursesAsync(string studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId, s => s.CourseRegistrations);
            return _mapper.Map<List<CourseRegistrationDto>>(student.CourseRegistrations).ToList() ?? new List<CourseRegistrationDto>();
        }

        public async Task<List<StudentDocumentDto>> GetStudentDocumentsAsync(string studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId, s => s.StudentDocuments);
            return _mapper.Map<List<StudentDocumentDto>>(student.CourseRegistrations).ToList() ?? new List<StudentDocumentDto>();
        }

        public async Task<bool> CompleteRegistrationAsync(string studentId)
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

        public async Task<PaginatedResult<StudentDto>> GetStudentsPagedAsync(int pageNumber, int pageSize, string term, int? departmentId, StudentStatus status)
        {
            var students = await _studentRepository.GetPagedAsync(pageNumber, pageSize);
            
            return new PaginatedResult<StudentDto>
            {
                Data = _mapper.Map<List<StudentDto>>(students),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = students.Count(),
                TotalPages = (int)Math.Ceiling(students.Count() / (double)pageSize)
            };
        }
        public async Task<List<InvoiceDto>> GetStudentInvoicesAsync(string studentId)
        {
            try
            {
                var invoices = new List<InvoiceDto>();
                var Students = await _studentRepository.GetByIdAsync(studentId,e => e.CourseRegistrations);
                foreach (var item in Students.CourseRegistrations)
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
    }
}
