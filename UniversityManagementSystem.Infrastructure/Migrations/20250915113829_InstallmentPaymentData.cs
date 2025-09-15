using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InstallmentPaymentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Program",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da2ed637-43e4-41e3-9dd6-2d550ee26624", "AQAAAAIAAYagAAAAEKtX94DoB6hi54mi8l5BVc4u4Pv2S9I9zwRYOhaHY3LD0Ebqx3J65iKiIgxw1DwRXQ==", "cf9911da-7d56-4ad5-9adb-eaeca8465ae5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Program",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "016b7347-04f8-4bbc-9b9f-5cee424d200b", "AQAAAAIAAYagAAAAEMlUY1lHVxAH70DK6mR1FOx5M4RGhMnIg8ahRwARcQts4vIixSsqhFxpWmrHFrWgNA==", "e1aacad6-981d-4107-985f-69352ee06d8d" });
        }
    }
}
