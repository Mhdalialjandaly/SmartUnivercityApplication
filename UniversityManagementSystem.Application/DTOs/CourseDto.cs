
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public decimal Fee { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Instructor { get; set; }
        public int MaxStudents { get; set; }
        public int CurrentStudents { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int Semester { get; set; }
        public int AcademicYear { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Prerequisites { get; set; }
        public string CourseType { get; set; }      
        public int GPA { get; set; }
        public int CreditHours { get; set; }
        public string SemesterName { get; set; }
        public int Capacity { get; set; }

        public int ProfessorId { get; set; }
        public ProfessorDto Professor { get; set; }
        public  Department Department { get; set; }
    }
    public class CourseSearchResult
    {
        public List<CourseDto> Courses { get; set; } 
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
