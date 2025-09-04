using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Professor : IEntity
    {
    
        public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public string ProfileImageUrl { get; set; }

        public Department Department { get; set; }

        public AcademicRank Rank { get; set; }

        [Required]
        [StringLength(100)]
        public string Specialization { get; set; }

        public DateTime HireDate { get; set; }

        public ProfessorStatus Status { get; set; } 
        public ICollection<Course> Courses { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }

}
