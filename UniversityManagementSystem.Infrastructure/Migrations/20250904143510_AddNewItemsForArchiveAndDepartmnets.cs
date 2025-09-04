using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewItemsForArchiveAndDepartmnets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveItems_Departments_DepartmentId",
                table: "ArchiveItems");

            migrationBuilder.AddColumn<int>(
                name: "CourseCount",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgramCount",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResearchProjectCount",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SuccessRate",
                table: "Departments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "ArchiveItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ArchiveItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ArchiveItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ArchiveItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentCount",
                table: "ArchiveItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "ArchiveItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "ArchiveItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FileCount",
                table: "ArchiveItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "ArchiveItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageCount",
                table: "ArchiveItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "ArchiveItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UnitType",
                table: "ArchiveItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64c9e1ab-4fc0-496c-bf69-e4555aba088e", "AQAAAAIAAYagAAAAEGbVZA1liCzAC9klsR7UuL1neeqY+V8TSwZbcoptWFiKGR4lZtcDyVC6faxygjOfLA==", "e17479fd-35a4-418b-841e-ec645bcb1399" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CourseCount", "IsActive", "Location", "ProgramCount", "ResearchProjectCount", "SuccessRate" },
                values: new object[] { 0, false, null, 0, 0, 0m });

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveItems_Departments_DepartmentId",
                table: "ArchiveItems",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveItems_Departments_DepartmentId",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "CourseCount",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ProgramCount",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ResearchProjectCount",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "SuccessRate",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "DepartmentCount",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "FileCount",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "PageCount",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ArchiveItems");

            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "ArchiveItems");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "ArchiveItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ac67809-5efe-4e4c-9d14-e650080cf006", "AQAAAAIAAYagAAAAEE6FKfHRLR0dPxB1eA/5Za8KhMBD68OgjRDGPw/GGSzO83bZoq5KG7vWWwJG6Iv7pQ==", "66ff7bd5-2069-42aa-b13d-d9015cc351b8" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveItems_Departments_DepartmentId",
                table: "ArchiveItems",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
