using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "149b2f7f-8358-4f68-be8e-e17eddb9f025", "149b2f7f-8358-4f68-be8e-e17eddb9f025", "Admin", "ADMIN" },
                    { "3bd3a155-1777-49bc-85bc-5745a6770252", "d5860d92-4315-40c6-8442-b45f192604e9", "Employee", "EMPLOYEE" },
                    { "3ca1e9ab-f2ac-4192-821a-dfd51193a1aa", "2820e5ae-25c3-4c0f-a3f8-32e452b1c95a", "User", "USER" },
                    { "dc0c5e04-6b9a-41fa-996f-57a5277d1a97", "25393912-38f0-4896-a095-e3386d3d96e3", "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "DeletedBy", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "ModifiedBy", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "51586e47-b125-4534-bba4-9bc6fd3dfbc8", 0, "cbecdff1-4390-4788-8e99-6595dbd208e9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin@mail.com", false, "Admin", false, null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ADMIN@MAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEDJPw0fIXEpTzTUQx9o+RvZOlY6bCzxWyK02oFEVCBckFWxwOEqsknoUoVidK0Osdw==", null, false, null, "3e524544-3c6a-406d-be0f-adfd0c0a1a2a", false, "Admin" });

            migrationBuilder.InsertData(
                table: "Tunnels",
                columns: new[] { "Id", "FirstPart", "Name", "SecoundPart", "TypeOfkinship" },
                values: new object[,]
                {
                    { 1, "-", "NoTunnel", "-", "-" },
                    { 2, "-", "ابن شهيد", "-", "اب" }
                });

            migrationBuilder.InsertData(
                table: "Universities",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "DeletedBy", "Description", "ModifiedAt", "ModifiedBy", "Name", "StudentId", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Premier university in UAE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "UAE University", null, null },
                    { 2, new DateTime(2025, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Premier university in SVU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SVU University", null, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "149b2f7f-8358-4f68-be8e-e17eddb9f025", "51586e47-b125-4534-bba4-9bc6fd3dfbc8" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "DeletedBy", "ModifiedAt", "ModifiedBy", "Name", "UniversityId" },
                values: new object[] { 1, "CS", new DateTime(2025, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Computer Science", 1 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Code", "CreatedAt", "Credits", "CurrentStudents", "DeletedAt", "DeletedBy", "DepartmentId", "Description", "Fee", "GPA", "Instructor", "IsActive", "MaxStudents", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, "CS101", new DateTime(2025, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, 0m, 0, null, false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Introduction to Programming" },
                    { 2, "CS201", new DateTime(2025, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, 0m, 0, null, false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Data Structures" },
                    { 3, "CS301", new DateTime(2025, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, 0m, 0, null, false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Algorithms" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bd3a155-1777-49bc-85bc-5745a6770252");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ca1e9ab-f2ac-4192-821a-dfd51193a1aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc0c5e04-6b9a-41fa-996f-57a5277d1a97");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "149b2f7f-8358-4f68-be8e-e17eddb9f025", "51586e47-b125-4534-bba4-9bc6fd3dfbc8" });

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tunnels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tunnels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "149b2f7f-8358-4f68-be8e-e17eddb9f025");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
