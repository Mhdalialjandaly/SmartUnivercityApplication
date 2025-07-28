using System.ComponentModel.DataAnnotations;
using UniversityManagementSystem.Domain.Entities.UniversityManagementSystem.Core.Entities;

namespace UniversityManagementSystem.Domain.Entities
{
    public class DocumentField : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FieldName { get; set; } 

        [StringLength(500)]
        public string FieldValue { get; set; } 

        [StringLength(50)]
        public string FieldType { get; set; }  // نص, تاريخ, رقم, قائمة

        public int DocumentId { get; set; }

        // العلاقات
        public virtual OfficialDocument Document { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
