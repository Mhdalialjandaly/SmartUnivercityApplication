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
    public class AssessmentServices : Injectable, IAssessmentServices
    {
        private readonly IRepository<Assessment> _assessmentRepository;
        private readonly IRepository<StudentAssessment> _studentAssessmentRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AssessmentServices(
            IRepository<Assessment> assessmentRepository,
            IRepository<StudentAssessment> studentAssessmentRepository,
            IRepository<Student> studentRepository,
            IRepository<Department> departmentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _assessmentRepository = assessmentRepository;
            _studentAssessmentRepository = studentAssessmentRepository;
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<AssessmentDto>> GetAssessmentsAsync(int pageNumber, int pageSize, string searchTerm = "")
        {
            try
            {
                var query = await _assessmentRepository.GetAllAsync();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(a =>
                        a.Title.Contains(searchTerm) ||
                        a.Code.Contains(searchTerm) ||
                        a.Description.Contains(searchTerm));
                }

                var totalCount = query.Count();
                var assessments = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var assessmentDtos = new List<AssessmentDto>();
                foreach (var assessment in assessments)
                {
                    var dto = await MapAssessmentToDto(assessment);
                    assessmentDtos.Add(dto);
                }

                return new PaginatedResult<AssessmentDto>
                {
                    Data = assessmentDtos,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalRecords = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving assessments: {ex.Message}", ex);
            }
        }

        public async Task<List<AssessmentDto>> GetAllAssessmentsAsync()
        {
            try
            {
                var assessments = await _assessmentRepository.GetAllAsync();
                var assessmentDtos = new List<AssessmentDto>();

                foreach (var assessment in assessments)
                {
                    var dto = await MapAssessmentToDto(assessment);
                    assessmentDtos.Add(dto);
                }

                return assessmentDtos.OrderBy(a => a.Date).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all assessments: {ex.Message}", ex);
            }
        }

        public async Task<List<AssessmentDto>> SearchAssessmentsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return await GetAllAssessmentsAsync();

                var assessments = (await _assessmentRepository.GetAllAsync())
                    .Where(a =>
                        a.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        a.Code.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        a.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var assessmentDtos = new List<AssessmentDto>();
                foreach (var assessment in assessments)
                {
                    var dto = await MapAssessmentToDto(assessment);
                    assessmentDtos.Add(dto);
                }

                return assessmentDtos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching assessments: {ex.Message}", ex);
            }
        }

        public async Task<AssessmentDto> GetAssessmentByIdAsync(int id)
        {
            try
            {
                var assessment = await _assessmentRepository.GetByIdAsync(id);
                if (assessment == null)
                    throw new Exception($"Assessment with ID {id} not found");

                return await MapAssessmentToDto(assessment);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving assessment by ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<AssessmentDto> CreateAssessmentAsync(AssessmentDto assessmentDto)
        {
            try
            {
                var assessment = _mapper.Map<Assessment>(assessmentDto);
                assessment.CreatedDate = DateTime.Now;

                await _assessmentRepository.AddAsync(assessment);
                await _unitOfWork.CommitAsync();

                return await MapAssessmentToDto(assessment);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating assessment: {ex.Message}", ex);
            }
        }

        public async Task UpdateAssessmentAsync(int id, AssessmentDto assessmentDto)
        {
            try
            {
                var existingAssessment = await _assessmentRepository.GetByIdAsync(id);
                if (existingAssessment == null)
                    throw new Exception($"Assessment with ID {id} not found");

                _mapper.Map(assessmentDto, existingAssessment);
                existingAssessment.UpdatedDate = DateTime.Now;

                _assessmentRepository.Update(existingAssessment);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating assessment with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task DeleteAssessmentAsync(int id)
        {
            try
            {
                var assessment = await _assessmentRepository.GetByIdAsync(id);
                if (assessment == null)
                    throw new Exception($"Assessment with ID {id} not found");

                _assessmentRepository.Delete(assessment);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting assessment with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<AssessmentStatsDto> GetAssessmentStatsAsync()
        {
            try
            {
                var assessments = await _assessmentRepository.GetAllAsync();
                var studentAssessments = await _studentAssessmentRepository.GetAllAsync();

                return new AssessmentStatsDto
                {
                    TotalAssessments = assessments.Count(),
                    CompletedAssessments = assessments.Count(a => a.Status == AssessmentStatus.Completed),
                    PendingAssessments = assessments.Count(a => a.Status == AssessmentStatus.Pending),
                    InProgressAssessments = assessments.Count(a => a.Status == AssessmentStatus.InProgress),
                    UpcomingAssessments = assessments.Count(a => a.Status == AssessmentStatus.Upcoming),
                    AverageCompletionRate = assessments.Any() ?
                        (decimal)assessments.Count(a => a.Status == AssessmentStatus.Completed) / assessments.Count() * 100 : 0,
                    TotalStudentSubmissions = studentAssessments.Count(),
                    AverageScore = studentAssessments.Any() ?
                        studentAssessments.Average(sa => sa.Score) : 0
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving assessment statistics: {ex.Message}", ex);
            }
        }

        public async Task<List<AssessmentDto>> GetUpcomingAssessmentsAsync(int days = 7)
        {
            try
            {
                var startDate = DateTime.Now;
                var endDate = DateTime.Now.AddDays(days);

                var assessments = (await _assessmentRepository.GetAllAsync())
                    .Where(a => a.Date >= startDate && a.Date <= endDate &&
                               (a.Status == AssessmentStatus.Pending || a.Status == AssessmentStatus.Upcoming))
                    .OrderBy(a => a.Date)
                    .ToList();

                var assessmentDtos = new List<AssessmentDto>();
                foreach (var assessment in assessments)
                {
                    var dto = await MapAssessmentToDto(assessment);
                    assessmentDtos.Add(dto);
                }

                return assessmentDtos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving upcoming assessments: {ex.Message}", ex);
            }
        }

        public async Task<List<StudentAssessmentDto>> GetStudentAssessmentsAsync(int studentId)
        {
            try
            {
                var studentAssessments = (await _studentAssessmentRepository.GetAllAsync())
                    .Where(sa => sa.StudentId == studentId)
                    .ToList();

                return _mapper.Map<List<StudentAssessmentDto>>(studentAssessments);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving student assessments for student ID {studentId}: {ex.Message}", ex);
            }
        }

        public async Task<bool> SubmitAssessmentResultAsync(int assessmentId, int studentId, decimal score, string feedback = "")
        {
            try
            {
                var studentAssessment = new StudentAssessment
                {
                    AssessmentId = assessmentId,
                    StudentId = studentId,
                    Score = score,
                    Feedback = feedback,
                    SubmissionDate = DateTime.Now
                };

                await _studentAssessmentRepository.AddAsync(studentAssessment);
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error submitting assessment result: {ex.Message}", ex);
            }
        }

        private async Task<AssessmentDto> MapAssessmentToDto(Assessment assessment)
        {
            var dto = _mapper.Map<AssessmentDto>(assessment);

            // جلب معلومات القسم
            if (assessment.DepartmentId.HasValue)
            {
                var department = await _departmentRepository.GetByIdAsync(assessment.DepartmentId.Value);
                dto.Department = department != null ? _mapper.Map<DepartmentDto>(department) : null;
            }

            // حساب إحصائيات الطلاب
            var studentAssessments = (await _studentAssessmentRepository.GetAllAsync())
                .Where(sa => sa.AssessmentId == assessment.Id)
                .ToList();

            dto.TotalStudents = studentAssessments.Count;
            dto.CompletedCount = studentAssessments.Count(sa => sa.Score > 0);
            dto.PendingCount = dto.TotalStudents - dto.CompletedCount;

            return dto;
        }
    }
}