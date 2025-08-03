using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhotoUrl", "SecurityStamp" },
                values: new object[] { "71017e31-fdae-441c-b403-175db1c8c044", "AQAAAAIAAYagAAAAECDMHzYjLUllefx5yHQAtw6pMQKp0WfJ5AVlPgt6tPbpQQrMk02uErnAXPHDArzNLw==", null, "c21a3dcf-7e6a-4adc-9f02-1fbc592da553" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 2, 13, 37, 56, 349, DateTimeKind.Local).AddTicks(2045));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 2, 13, 37, 56, 349, DateTimeKind.Local).AddTicks(2063));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 2, 13, 37, 56, 349, DateTimeKind.Local).AddTicks(2065));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "caae4e25-38c3-48ab-b103-3dc936fda7e4", "AQAAAAIAAYagAAAAEIYQFT8QzLno45T6gX96kLyd8HT65sUWtwycTE12BoBC0vCjRhpt477Nx+FU6P0akw==", "c967df7f-94bf-4e95-b877-ab4b8af8da3a" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 1, 14, 31, 37, 793, DateTimeKind.Local).AddTicks(3058));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 1, 14, 31, 37, 793, DateTimeKind.Local).AddTicks(3083));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 1, 14, 31, 37, 793, DateTimeKind.Local).AddTicks(3085));
        }
    }
}
