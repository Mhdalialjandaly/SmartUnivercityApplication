using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Domain.Entities
{
    public class StudentPayment : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }  // نقداً, تحويل بنكي, بطاقة

        [StringLength(100)]
        public string TransactionId { get; set; } 

        [StringLength(200)]
        public string Notes { get; set; } 

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // مدفوع, معلق, مرفوض

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
