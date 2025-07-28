using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addnewValueForCourseRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "CourseFee",
                table: "CourseRegistrations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "CourseRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CourseRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "CourseRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "CourseRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "CourseRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "CourseRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "CourseRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e32fdca-fbea-49d1-9ed5-02314c23f183", "aeb70d30-59fa-4312-a3ad-a6a6b02fd762", "Employee", "EMPLOYEE" },
                    { "ad32e54b-a44e-4600-8452-e3fff933f296", "bdf40004-82f1-4ff2-ad68-b85056cc5efe", "Student", "STUDENT" },
                    { "ae2e8fe2-fa38-4332-b073-3205ca0f1ac5", "a27847c8-c180-40de-9862-bc4e03f8377a", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16666ab2-5652-429f-ae97-83d2c7257968", "AQAAAAIAAYagAAAAEDEgxPkq4qwRctlQC0xsEr+icqGXLCCKq5Yuzeg4Iv4TJUk6ivqyCDQX8ik+gVcuUw==", "9c324b5b-10f1-4886-af5f-0eb4548d857c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e32fdca-fbea-49d1-9ed5-02314c23f183");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad32e54b-a44e-4600-8452-e3fff933f296");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae2e8fe2-fa38-4332-b073-3205ca0f1ac5");

            migrationBuilder.DropColumn(
                name: "CourseFee",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "CourseRegistrations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bd3a155-1777-49bc-85bc-5745a6770252", "d5860d92-4315-40c6-8442-b45f192604e9", "Employee", "EMPLOYEE" },
                    { "3ca1e9ab-f2ac-4192-821a-dfd51193a1aa", "2820e5ae-25c3-4c0f-a3f8-32e452b1c95a", "User", "USER" },
                    { "dc0c5e04-6b9a-41fa-996f-57a5277d1a97", "25393912-38f0-4896-a095-e3386d3d96e3", "Student", "STUDENT" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cbecdff1-4390-4788-8e99-6595dbd208e9", "AQAAAAIAAYagAAAAEDJPw0fIXEpTzTUQx9o+RvZOlY6bCzxWyK02oFEVCBckFWxwOEqsknoUoVidK0Osdw==", "3e524544-3c6a-406d-be0f-adfd0c0a1a2a" });
        }
    }
}
