using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "LastLoginDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7dda9331-01fc-4f98-8683-bdfe86ad1a43", null, "AQAAAAIAAYagAAAAEG/bc6P7paQKuNNvlygIAGVqMKF6PfruyHOnutR/LX1MZH8+chBhPtG56O48ZEq8Qg==", "bdd7bc1d-7c9e-44a5-b804-b3f1c91b34af" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4bc490bd-92e5-4c90-b57d-c18a2b343f31", "AQAAAAIAAYagAAAAEO4auMtmE74z+p8qHsUqSRNeSPjhpH1KXPr2r2/hB2sdXShN1kF5otBYQMTu8CCSaA==", "4b4c8e9f-5fc4-49bf-ae06-6a7cada8f1f2" });
        }
    }
}
