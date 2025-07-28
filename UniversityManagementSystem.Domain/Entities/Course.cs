using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Course : IEntity
    {
        public Course()
        {
            CourseRegistrations = new HashSet<CourseRegistration>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; } 

        [Required]
        [StringLength(200)]
        public string Name { get; set; } 

        public int Credits { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Fee { get; set; }

        public int DepartmentId { get; set; }

        [StringLength(100)]
        public string Instructor { get; set; }
        public string Description { get; set; }

        public int MaxStudents { get; set; }

        public int CurrentStudents { get; set; }
        public int GPA { get; set; }

        public bool IsActive { get; set; }     

        public int Semester { get; set; }

        public int AcademicYear { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string Prerequisites { get; set; } = "";

        [StringLength(50)]
        public string CourseType { get; set; } = "نظري"; // نظري, عملي, نظري وعملي

      
        public virtual Department Department { get; set; }
        public virtual ICollection<CourseRegistration> CourseRegistrations { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
