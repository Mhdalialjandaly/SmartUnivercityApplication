using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Entities.UniversityManagementSystem.Core.Entities;

namespace UniversityManagementSystem.Infrastructure.Data
{
    public partial class UniversityDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<FinanceRecord> FinanceRecord { get; set; }
        public DbSet<StudentApplication> StudentApplications { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseRegistration> CourseRegistrations { get; set; }
        public DbSet<Tunnel> Tunnels { get; set; }
        public DbSet<StudentDocument> StudentDocuments { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<OfficialDocument> OfficialDocuments { get; set; }
        public DbSet<DocumentTemplate> DocumentTemplates { get; set; }
        public DbSet<DocumentField> DocumentFields { get; set; }
        public DbSet<DocumentSignature> DocumentSignatures { get; set; }
        public DbSet<StudentPayment> StudentPayments { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>().ToTable("Students");
            builder.Entity<Department>().ToTable("Departments");
                
            builder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            OnModelCreatingPartial(builder);
			
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
