using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProffessorsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Professors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15d2693f-e008-4952-ad2b-ffbc798f910c", "AQAAAAIAAYagAAAAEL/aeK0VpqEtVXWV51F5iTgire2zO2hguC/FzRKDKlmlfI1S2gFczf62Nql94Jnn8g==", "d8ef01d0-4609-4ea2-97da-1660ca12c25c" });

            migrationBuilder.UpdateData(
                table: "Professors",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProfileImageUrl",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Professors");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7dda9331-01fc-4f98-8683-bdfe86ad1a43", "AQAAAAIAAYagAAAAEG/bc6P7paQKuNNvlygIAGVqMKF6PfruyHOnutR/LX1MZH8+chBhPtG56O48ZEq8Qg==", "bdd7bc1d-7c9e-44a5-b804-b3f1c91b34af" });
        }
    }
}
