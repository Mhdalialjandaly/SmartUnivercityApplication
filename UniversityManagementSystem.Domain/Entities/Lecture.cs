using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Lecture :IEntity
    {
        public Lecture() 
        {
            Attendances = new HashSet<Attendance>();
        }
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } 

        [StringLength(1000)]
        public string Description { get; set; } 

        [Required]
        public DateTime LectureDate { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [StringLength(100)]
        public string Location { get; set; } 

        [StringLength(50)]
        public string LectureType { get; set; } 

        public bool IsCancelled { get; set; } 

        [StringLength(500)]
        public string Notes { get; set; } 

        public DateTime CreatedDate { get; set; }

        // العلاقات
        public virtual Course Course { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
