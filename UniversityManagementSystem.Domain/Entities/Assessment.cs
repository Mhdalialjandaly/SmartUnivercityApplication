
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Assessment : IEntity
    {
        public Assessment() 
        {
            StudentAssessments = new HashSet<StudentAssessment>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; } 
        public AssessmentType Type { get; set; }
        public AssessmentStatus Status { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<StudentAssessment> StudentAssessments { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
