using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Course : IEntity
    {
        public Course()
        {
            CourseRegistrations = new HashSet<CourseRegistration>();
            Schedules = new HashSet<Schedule>();
            Lectures = new HashSet<Lecture>();
            StudentPayments = new HashSet<StudentPayment>();
            StudentAttendances = new HashSet<StudentAttendance>();
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

        [StringLength(100)]
        public string Instructor { get; set; }
        public string Description { get; set; }

        public int MaxStudents { get; set; }

        public int CurrentStudents { get; set; }
        public int GPA { get; set; }

        public bool IsActive { get; set; }     

        public int Semester { get; set; }

        public int AcademicYear { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(100)]
        public string Prerequisites { get; set; } 

        [StringLength(50)]
        public string CourseType { get; set; } 


        public int CreditHours { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int EnrolledStudentsCount { get; set; }
        public double EnrollmentPercentage { get; set; }
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }

        public string SemesterName { get; set; }
        public int Capacity { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<CourseRegistration> CourseRegistrations { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual ICollection<StudentPayment>StudentPayments { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
