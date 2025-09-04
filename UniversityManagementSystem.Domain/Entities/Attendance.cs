using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Attendance:IEntity
    {
        public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal HoursWorked { get; set; }

        [StringLength(20)]
        public string Status { get; set; } // حاضر, غائب, إجازة, تأخير

        [StringLength(200)]
        public string Notes { get; set; }
        public int StudentId { get; set; }
        public int LectureId { get; set; }
        public bool IsPresent { get; set; }
        // العلاقات
        public  User Employee { get; set; }
        public Student Student { get; set; }
        public Lecture Lecture { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}
