using System.ComponentModel.DataAnnotations;
using UniversityManagementSystem.Domain.Entities.UniversityManagementSystem.Core.Entities;

namespace UniversityManagementSystem.Domain.Entities
{
    public class DocumentSignature : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string SignerName { get; set; }

        [StringLength(100)]
        public string SignerPosition { get; set; }

        public string SignatureData { get; set; }

        public DateTime SignatureDate { get; set; }

        [StringLength(50)]
        public string SignatureType { get; set; }  // رقمي, يدوي

        public int DocumentId { get; set; }

        // العلاقات
        public virtual OfficialDocument Document { get; set; }
        public DateTime SignedDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        DateTime IEntity.DeletedAt { get ; set ; }
        DateTime IEntity.ModifiedAt { get ; set ; }
    }
}
