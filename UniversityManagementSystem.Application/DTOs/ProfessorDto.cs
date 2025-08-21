
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class ProfessorDto
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public AcademicRank Rank { get; set; }
        public string Specialization { get; set; }
        public DateTime HireDate { get; set; }
        public ProfessorStatus Status { get; set; }
        public int WeeklyHours { get; set; }
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
        public List<ScheduleDto> Schedules { get; set; } = new List<ScheduleDto>();
    }
}
