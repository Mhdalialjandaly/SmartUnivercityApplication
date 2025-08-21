namespace UniversityManagementSystem.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    namespace UniversityManagementSystem.Core.Entities
    {
        public class OfficialDocument:IEntity   
        {
            public OfficialDocument() 
            {
                DocumentFields = new HashSet<DocumentField>();
                DocumentSignatures = new HashSet<DocumentSignature>();
            }
            public int Id { get; set; }

            [Required]
            [StringLength(200)]
            public string Title { get; set; } 

            [Required]
            public string Content { get; set; } 

            [Required]
            [StringLength(50)]
            public string DocumentType { get; set; } 

            [StringLength(100)]
            public string TemplateName { get; set; } 

            public DateTime CreatedDate { get; set; } 

            [StringLength(100)]
            public string CreatedBy { get; set; } 

            public DateTime? IssuedDate { get; set; }

            [StringLength(100)]
            public string IssuedTo { get; set; } 

            [StringLength(50)]
            public string DocumentNumber { get; set; } 

            public string SignatureData { get; set; } 

            public string Status { get; set; }  

            public string FilePath { get; set; } 

            // العلاقات
            public virtual ICollection<DocumentField> DocumentFields { get; set; } 
            public virtual ICollection<DocumentSignature> DocumentSignatures { get; set; } 
            public DateTime CreatedAt { get ; set ; }
            public DateTime DeletedAt { get ; set ; }
            public DateTime ModifiedAt { get ; set ; }
            public string ModifiedBy { get ; set ; }
            public string DeletedBy { get ; set ; }
        }
    }
}
