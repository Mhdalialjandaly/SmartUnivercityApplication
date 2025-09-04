using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditFormal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AttendancePercentage",
                table: "Students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92d8e946-707b-42a0-83be-6ebc3c8497d4", "AQAAAAIAAYagAAAAELA85HSp89qME/PrA9FtwPRVkGjRyr2gYxBCPHFu77vXM713VJICTJr0h+Mplp9y+Q==", "86ffc3b2-783c-4e09-898d-a71554570dac" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendancePercentage",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15d2693f-e008-4952-ad2b-ffbc798f910c", "AQAAAAIAAYagAAAAEL/aeK0VpqEtVXWV51F5iTgire2zO2hguC/FzRKDKlmlfI1S2gFczf62Nql94Jnn8g==", "d8ef01d0-4609-4ea2-97da-1660ca12c25c" });
        }
    }
}
