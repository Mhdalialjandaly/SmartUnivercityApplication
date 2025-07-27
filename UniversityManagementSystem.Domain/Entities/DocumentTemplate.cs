
namespace UniversityManagementSystem.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    namespace UniversityManagementSystem.Core.Entities
    {
        public class DocumentTemplate : IEntity
        {
            public int Id { get; set; }            
            public string Name { get; set; } 
            public string HtmlContent { get; set; } 
            public string DocumentType { get; set; } 

            public DateTime CreatedDate { get; set; }
            public string CreatedBy { get; set; } 
            public bool IsActive { get; set; } 
            public string Description { get; set; } 
            public DateTime CreatedAt { get ; set ; }
            public DateTime DeletedAt { get ; set ; }
            public DateTime ModifiedAt { get ; set ; }
            public string ModifiedBy { get ; set ; }
            public string DeletedBy { get ; set ; }
        }
    }
}
