using Microsoft.EntityFrameworkCore;
namespace UniversityManagementSystem.Infrastructure.Data
{
    public partial class UniversityDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
        }
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=SmartUniverCityDataBase;User Id=sa;Password=Asd123zxc;TrustServerCertificate=true;");
        }
    }
}
