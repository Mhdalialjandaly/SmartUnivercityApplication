using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Infrastructure.Data
{
    public static class UniversityDbInitializer
    {
        private const string UserAdminId = "51586e47-b125-4534-bba4-9bc6fd3dfbc8";
        private const string AdminRoleId = "149b2f7f-8358-4f68-be8e-e17eddb9f025";
        private static readonly DateTime SeedDate = new DateTime(2025, 7, 24); // Fixed date format

        public static void SeedData(this ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder);
            SeedAdminUser(modelBuilder);
            SeedUniversityData(modelBuilder);
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = AdminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = AdminRoleId // Using role ID as concurrency stamp
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Student",
                    NormalizedName = "STUDENT",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );
        }

        private static void SeedAdminUser(ModelBuilder modelBuilder)
        {
            //var adminUser = new User
            //{
            //    Id = UserAdminId,
            //    UserName = "admin",
            //    FirstName = "Admin",
            //    LastName = "User",
            //    NormalizedUserName = "ADMIN",
            //    Email = "admin@university.com",
            //    NormalizedEmail = "ADMIN@UNIVERSITY.COM",
            //    EmailConfirmed = true,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    PasswordHash = new PasswordHasher<User>().HashPassword(null, "Admin@12345")
            //};
            var adminUser = GenerateUser(UserAdminId, "Admin", "Admin@mail.com", "Admin", "Admin@12345");
            modelBuilder.Entity<User>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> 
                {
                    UserId = adminUser.Id,
                    RoleId = AdminRoleId
                });
        }

        private static void SeedUniversityData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>().HasData(new List<University>() {
                new University
                {
                    Id = 1,
                    Name = "UAE University",
                    Description = "Premier university in UAE",
                    CreatedAt = SeedDate
                } ,
                 new University
                {
                    Id = 2,
                    Name = "SVU University",
                    Description = "Premier university in SVU",
                    CreatedAt = SeedDate
                }
            });

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Code = "CS",
                    Name = "Computer Science",
                    UniversityId = 1,
                    CreatedAt = SeedDate
                });

            modelBuilder.Entity<Course>().HasData(new List<Course>() {
                new Course
                {
                    Id = 1,
                    Code = "CS101",
                    Name = "Introduction to Programming",
                    DepartmentId = 1,
                    CreatedAt = SeedDate
                },
                new Course
                {
                    Id = 2,
                    Code = "CS201",
                    Name = "Data Structures",
                    DepartmentId = 1,
                    CreatedAt = SeedDate
                },
                new Course
                {
                    Id = 3,
                    Code = "CS301",
                    Name = "Algorithms",
                    DepartmentId = 1,
                    CreatedAt = SeedDate
                } });

            modelBuilder.Entity<Tunnel>().HasData(new List<Tunnel>() {
            new Tunnel
            {
                Id = 1,
                Name="NoTunnel",
                TypeOfkinship="-",
                FirstPart="-",
                SecoundPart="-"
            }, 
                new Tunnel
            {
                Id = 2,
                Name ="ابن شهيد",
                TypeOfkinship="اب",
                FirstPart="-",
                SecoundPart="-"
            }
            });
        }
        private static User GenerateUser(string id, string userName, string email, string role, string password)
        {
            var adminUser = new User
            {
                Id = id,
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = false,
                FirstName = userName,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false
            };
            adminUser.PasswordHash = new PasswordHasher<User>().HashPassword(adminUser, password);
            return adminUser;
        }
    }
}