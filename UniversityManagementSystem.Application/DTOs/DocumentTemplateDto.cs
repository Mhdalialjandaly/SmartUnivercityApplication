
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Application.DTOs
{
    public class DocumentTemplateDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string HtmlContent { get; set; }

        [StringLength(50)]
        public string DocumentType { get; set; }

        [StringLength(100)]
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } 
        public bool IsActive { get; set; }
        public string Description { get; set; } 
    }
}
