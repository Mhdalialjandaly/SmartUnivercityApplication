using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Infrastructure.Data
{
    public static class UniversityDbInitializer
    {
        private const string UserAdminId = "51586e47-b125-4534-bba4-9bc6fd3dfbc8";
        private const string AdminRoleId = "149b2f7f-8358-4f68-be8e-e17eddb9f025";
        private const string UserRoleId = "249b2f7f-8358-4f68-be8e-e17eddb9f026";
        private const string EmployeeRoleId = "349b2f7f-8358-4f68-be8e-e17eddb9f027";
        private const string StudentRoleId = "449b2f7f-8358-4f68-be8e-e17eddb9f028";
        private static readonly DateTime SeedDate = new DateTime(2025, 7, 24);

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
                    ConcurrencyStamp = AdminRoleId
                },
                new IdentityRole
                {
                    Id = UserRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = UserRoleId
                },
                new IdentityRole
                {
                    Id = EmployeeRoleId,
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                    ConcurrencyStamp = EmployeeRoleId
                },
                new IdentityRole
                {
                    Id = StudentRoleId,
                    Name = "Student",
                    NormalizedName = "STUDENT",
                    ConcurrencyStamp = StudentRoleId
                }
            );
        }

        private static void SeedAdminUser(ModelBuilder modelBuilder)
        {
            // إنشاء مستخدم Admin
            var adminUser = GenerateUser(UserAdminId, "admin@12345.com", "admin@university.com", "Admin", "Admin@12345");

            modelBuilder.Entity<User>().HasData(adminUser);

            // إعطاء المستخدم دور Admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUser.Id,
                    RoleId = AdminRoleId
                });
        }

        private static void SeedUniversityData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>().HasData(
                new University
                {
                    Id = 1,
                    Name = "UAE University",
                    Description = "Premier university in UAE",
                    CreatedAt = SeedDate
                },
                new University
                {
                    Id = 2,
                    Name = "SVU University",
                    Description = "Premier university in SVU",
                    CreatedAt = SeedDate
                }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Code = "CS",
                    Name = "Computer Science",
                    UniversityId = 1,
                    CreatedAt = SeedDate
                }
            );

            modelBuilder.Entity<Course>().HasData(
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
                }
            );

            modelBuilder.Entity<Tunnel>().HasData(
                new Tunnel
                {
                    Id = 1,
                    Name = "NoTunnel",
                    TypeOfkinship = "-",
                    FirstPart = "-",
                    SecoundPart = "-"
                },
                new Tunnel
                {
                    Id = 2,
                    Name = "ابن شهيد",
                    TypeOfkinship = "اب",
                    FirstPart = "-",
                    SecoundPart = "-"
                }
            );
        }

        private static User GenerateUser(string id, string userName, string email, string firstName, string password)
        {
            var user = new User
            {
                Id = id,
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true, // تأكيد البريد الإلكتروني تلقائياً
                FirstName = firstName,
                LastName = "Administrator", // إضافة اسم العائلة
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(), // إضافة SecurityStamp
                LockoutEnabled = true,
                AccessFailedCount = 0,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                CreatedAt = SeedDate // إضافة تاريخ الإنشاء
            };

            // تشفير كلمة المرور
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, password);
            return user;
        }
    }
}