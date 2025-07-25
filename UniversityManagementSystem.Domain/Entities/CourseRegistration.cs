using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Domain.Entities
{
    public class CourseRegistration
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string Status { get; set; } = "مسجل"; // مسجل, ملغي, مكتمل

        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; } = 0;

        public DateTime PaymentDate { get; set; }

        // العلاقات
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
