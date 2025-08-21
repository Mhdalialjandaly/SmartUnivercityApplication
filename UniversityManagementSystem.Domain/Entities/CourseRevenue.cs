namespace UniversityManagementSystem.Domain.Entities
{
    public class CourseRevenue : IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public int CurrentStudents { get; set; }

        public decimal Fee { get; set; }

        public decimal TotalRevenue { get; set; }

        public int Semester { get; set; }

        public int Year { get; set; }

        public decimal Percentage { get; set; }

        // Navigation Properties
        public virtual Department Department { get; set; } = null!;
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
