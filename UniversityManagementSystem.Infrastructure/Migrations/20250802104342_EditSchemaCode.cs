using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditSchemaCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c75dd8e6-fa82-4f04-9006-0c98c6fb63c5", "AQAAAAIAAYagAAAAEPnfni5wSAx0rN3gmoVIe/dP/j+WCjqLT9ptStQkEJuHqwp9OZUjo0hUayyMUzXNZw==", "6677354c-8abf-47ff-8230-d8d25b50a9e2" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 2, 13, 43, 41, 768, DateTimeKind.Local).AddTicks(8523));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 2, 13, 43, 41, 768, DateTimeKind.Local).AddTicks(8647));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 2, 13, 43, 41, 768, DateTimeKind.Local).AddTicks(8649));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71017e31-fdae-441c-b403-175db1c8c044", "AQAAAAIAAYagAAAAECDMHzYjLUllefx5yHQAtw6pMQKp0WfJ5AVlPgt6tPbpQQrMk02uErnAXPHDArzNLw==", "c21a3dcf-7e6a-4adc-9f02-1fbc592da553" });

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
    }
}
