using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.DTOs
{
    public class CourseDetailDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string DepartmentName { get; set; }
        public int StudentCount { get; set; }
        public double AverageScore { get; set; }
        public double AttendanceRate { get; set; }
        public double SuccessRate { get; set; }
        public double AverageRating { get; set; }
        public int Credits { get; set; }
        public string ProfessorName { get; set; }
        public int Semester { get; set; }
        public int AcademicYear { get; set; }
        public List<EnrollmentDto> Enrollments { get; set; }
        public List<CourseEvaluationDto> CourseEvaluations { get; set; }
    }
}
