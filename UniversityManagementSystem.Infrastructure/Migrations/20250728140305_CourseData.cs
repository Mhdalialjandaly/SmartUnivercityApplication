using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "AcademicYear",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CourseType",
                table: "Courses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Prerequisites",
                table: "Courses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LectureId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Attendances",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LectureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LectureType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d93149c-f18f-4de2-9ab6-725f6ea0f12f", "595bd8d9-2db7-4a56-8b8e-727e27ea9e86", "Student", "STUDENT" },
                    { "a39c2f21-730d-42c5-ae3f-e24621765145", "dd8c14fb-25a3-49fa-aeca-f7af72a58842", "User", "USER" },
                    { "bb9aea7e-2855-4d2a-b176-39f892109c65", "0175fcf9-bec0-4fc1-82af-7b90e7e4269d", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "208d39b1-2fb2-4f85-a352-5142f255e3b9", "AQAAAAIAAYagAAAAEBrgL17p65B73t4wwG/4VJrfSA+IcCQMIAd2i8KWRQmN7s4C9DnQ5bAgOe5t5Bd0sg==", "1e97001a-ba9c-4c35-8b87-df9ef7993480" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AcademicYear", "CourseType", "CreatedDate", "Prerequisites", "Semester" },
                values: new object[] { 0, "نظري", new DateTime(2025, 7, 28, 17, 3, 3, 652, DateTimeKind.Local).AddTicks(1776), "", 0 });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AcademicYear", "CourseType", "CreatedDate", "Prerequisites", "Semester" },
                values: new object[] { 0, "نظري", new DateTime(2025, 7, 28, 17, 3, 3, 652, DateTimeKind.Local).AddTicks(1832), "", 0 });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AcademicYear", "CourseType", "CreatedDate", "Prerequisites", "Semester" },
                values: new object[] { 0, "نظري", new DateTime(2025, 7, 28, 17, 3, 3, 652, DateTimeKind.Local).AddTicks(1836), "", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_LectureId",
                table: "Attendances",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_CourseId",
                table: "Lectures",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Lectures_LectureId",
                table: "Attendances",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Lectures_LectureId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_LectureId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d93149c-f18f-4de2-9ab6-725f6ea0f12f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a39c2f21-730d-42c5-ae3f-e24621765145");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb9aea7e-2855-4d2a-b176-39f892109c65");

            migrationBuilder.DropColumn(
                name: "AcademicYear",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseType",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Prerequisites",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LectureId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Attendances");

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
    }
}
