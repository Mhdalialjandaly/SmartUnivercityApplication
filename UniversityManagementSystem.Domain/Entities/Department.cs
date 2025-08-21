namespace UniversityManagementSystem.Domain.Entities
{
    public class Department : IEntity
    {
        public Department() { 
            Students = new HashSet<Student>();
            Courses = new HashSet<Course>();
            FinanceRecords = new HashSet<FinanceRecord>();
            Professors = new HashSet<Professor>();
            AcademicCalendars =new HashSet<AcademicCalendar>();
            StudentApplications = new HashSet<StudentApplication>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int UniversityId { get; set; }
        public University University { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public ICollection<Professor> Professors { get; set; }
        public virtual ICollection<FinanceRecord> FinanceRecords { get; set; }
        public virtual ICollection<AcademicCalendar> AcademicCalendars { get; set; }
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }

        public string Dean { get; set; }

        public int StudentCount { get; set; }

        public int FacultyCount { get; set; }

        public string Description { get; set; }

        // Navigation Properties
        public virtual ICollection<StudentPayment> StudentPayments { get; set; }
        public virtual ICollection<EmployeeSalary>  EmployeeSalaries { get; set; }
        public virtual ICollection<FinancialReport> FinancialReports { get; set; }
        public virtual ICollection<ScheduledReport> ScheduledReports { get; set; }
        public virtual ICollection<CourseRevenue> CourseRevenues { get; set; }
    }
}
