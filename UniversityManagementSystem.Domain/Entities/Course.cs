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
        public string Code { get; set; } = "";

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = "";

        public int Credits { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Fee { get; set; }

        public int DepartmentId { get; set; }

        [StringLength(100)]
        public string Instructor { get; set; } = "";

        public int MaxStudents { get; set; } = 30;

        public int CurrentStudents { get; set; } = 0;
        public int GPA { get; set; }
        // العلاقات
        public virtual Department Department { get; set; }
        public virtual ICollection<CourseRegistration> CourseRegistrations { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
