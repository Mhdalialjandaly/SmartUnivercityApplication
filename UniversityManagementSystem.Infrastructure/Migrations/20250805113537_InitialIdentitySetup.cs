using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentitySetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c245be8-f92f-48b1-a40d-2edb99c0dc5d", "AQAAAAIAAYagAAAAEFW+UIC/j6nklf2tOHgh7KLwV1aTdsNtjQ/gj7nE4KN3ji8lIj9OIHekovm27FMECg==", "4c6df599-e0d1-4b63-a3c3-d4d32b3244ae" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 5, 14, 35, 29, 615, DateTimeKind.Local).AddTicks(367));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 5, 14, 35, 29, 615, DateTimeKind.Local).AddTicks(403));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 5, 14, 35, 29, 615, DateTimeKind.Local).AddTicks(405));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e80abc0-e13c-4e23-839c-4eeec7421a3f", "AQAAAAIAAYagAAAAEGFSgd4yLWNV84CLN4qdpqLvaOUcqXobHjnwx32VAf5xaE1UayECiY0AaPPIBkx7/w==", "f192075b-bacb-4ef0-a150-f7aae1c9d515" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 2, 17, 47, 29, 900, DateTimeKind.Local).AddTicks(6443));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 2, 17, 47, 29, 900, DateTimeKind.Local).AddTicks(6483));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 2, 17, 47, 29, 900, DateTimeKind.Local).AddTicks(6485));
        }
    }
}
