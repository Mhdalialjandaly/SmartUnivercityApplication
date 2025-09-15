using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSalariesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaries_AspNetUsers_EmployeeId1",
                table: "EmployeeSalaries");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaries_Departments_DepartmentId",
                table: "EmployeeSalaries");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaries_EmployeeId1",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "EmployeeSalaries");

            migrationBuilder.RenameColumn(
                name: "SalaryDate",
                table: "EmployeeSalaries",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "EmployeeSalaries",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "EmployeePosition",
                table: "EmployeeSalaries",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "Deductions",
                table: "EmployeeSalaries",
                newName: "TotalDeductions");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "EmployeeSalaries",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "EmployeeSalaries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "EmployeeSalaries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "BankAccount",
                table: "EmployeeSalaries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPaymentDate",
                table: "EmployeeSalaries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalaryDeductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeSalaryId = table.Column<int>(type: "int", nullable: false),
                    DeductionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeductionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryDeductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeSalaryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPayments", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "016b7347-04f8-4bbc-9b9f-5cee424d200b", "AQAAAAIAAYagAAAAEMlUY1lHVxAH70DK6mR1FOx5M4RGhMnIg8ahRwARcQts4vIixSsqhFxpWmrHFrWgNA==", "e1aacad6-981d-4107-985f-69352ee06d8d" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaries_Departments_DepartmentId",
                table: "EmployeeSalaries",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaries_Departments_DepartmentId",
                table: "EmployeeSalaries");

            migrationBuilder.DropTable(
                name: "SalaryDeductions");

            migrationBuilder.DropTable(
                name: "SalaryPayments");

            migrationBuilder.DropColumn(
                name: "BankAccount",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "LastPaymentDate",
                table: "EmployeeSalaries");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "EmployeeSalaries",
                newName: "SalaryDate");

            migrationBuilder.RenameColumn(
                name: "TotalDeductions",
                table: "EmployeeSalaries",
                newName: "Deductions");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "EmployeeSalaries",
                newName: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "EmployeeSalaries",
                newName: "EmployeePosition");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "EmployeeSalaries",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeSalaries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "EmployeeSalaries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Bonus",
                table: "EmployeeSalaries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId1",
                table: "EmployeeSalaries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "EmployeeSalaries",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "EmployeeSalaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d65a27bd-bb95-4c32-b909-c17b3df801b3", "AQAAAAIAAYagAAAAEBRKo5LG22tlu0x2tXu0cvDtVl0cSKMl6R9GAe04JtWbraHI3pJFcNYV/rtbzIRa3w==", "c1f6d09c-2d0f-4af1-b1c3-8d040f6560ea" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_EmployeeId1",
                table: "EmployeeSalaries",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaries_AspNetUsers_EmployeeId1",
                table: "EmployeeSalaries",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaries_Departments_DepartmentId",
                table: "EmployeeSalaries",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
