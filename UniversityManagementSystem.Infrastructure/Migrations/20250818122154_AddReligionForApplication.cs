using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReligionForApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "StudentApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "357e3127-b5b8-48c5-8b13-ef6629a1c27c", "AQAAAAIAAYagAAAAECA9KDATWWcwnzlS3lDHgP2RSIIQchmpoEd9PTUn6kKYWDJESWDxnan2yLtQS4Lr0g==", "b037d73b-719d-4813-807b-f861912a5045" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Religion",
                table: "StudentApplications");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d2441b2-5d61-4682-bd50-b695ef3677d8", "AQAAAAIAAYagAAAAEMVQ7PAIvugiN4inZGXyfkQAHWFTI8MfYCb9cx5kK5OjL5VIHwWzOF9rlMe2MrKCiQ==", "bfbebf84-3d6c-4a0e-9daa-344e4f516f2f" });
        }
    }
}
