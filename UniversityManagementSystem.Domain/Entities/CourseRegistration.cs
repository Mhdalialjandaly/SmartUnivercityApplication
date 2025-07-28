using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Domain.Entities
{
    public class CourseRegistration :IEntity
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime RegistrationDate { get; set; }

        [StringLength(20)]
        public string Status { get; set; }  // مسجل, ملغي, مكتمل

        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; } 

        public DateTime PaymentDate { get; set; }
     
        public string StudentName { get; set; }
        public string CourseName { get; set; } 
        public decimal CourseFee { get; set; } 
        public string DepartmentName { get; set; } 

        // العلاقات
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
