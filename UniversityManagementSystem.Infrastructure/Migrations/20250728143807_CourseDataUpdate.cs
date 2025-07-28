using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourseDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d93149c-f18f-4de2-9ab6-725f6ea0f12f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a39c2f21-730d-42c5-ae3f-e24621765145");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb9aea7e-2855-4d2a-b176-39f892109c65");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Lectures",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Lectures",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4cbf2421-de7b-4f8d-9109-63c46d748a02", "e0280f2f-d041-4f85-a861-fa1c92aa741a", "Employee", "EMPLOYEE" },
                    { "7aec5499-fec2-4abf-9c59-ec436f07dbb9", "699e2256-fa27-4f31-aec0-b8c3385f1a18", "Student", "STUDENT" },
                    { "d1346b74-9166-40e2-969d-f4238971c4c0", "360f0ea7-ba0a-4776-82e7-f561ddb03a57", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "040a3224-6453-4152-b258-99c51a90117c", "AQAAAAIAAYagAAAAEJPppJXyEwYnHdNB3ONp+87sGQIYiYlf6nulv9HWGMuE4spCKK2Byu6NlrSRUWWfGw==", "ec1fd606-8293-4d00-b913-3d6acb509500" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 28, 17, 37, 57, 704, DateTimeKind.Local).AddTicks(260));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 28, 17, 37, 57, 704, DateTimeKind.Local).AddTicks(311));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 28, 17, 37, 57, 704, DateTimeKind.Local).AddTicks(315));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4cbf2421-de7b-4f8d-9109-63c46d748a02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7aec5499-fec2-4abf-9c59-ec436f07dbb9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1346b74-9166-40e2-969d-f4238971c4c0");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Lectures",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Lectures",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d93149c-f18f-4de2-9ab6-725f6ea0f12f", "595bd8d9-2db7-4a56-8b8e-727e27ea9e86", "Student", "STUDENT" },
                    { "a39c2f21-730d-42c5-ae3f-e24621765145", "dd8c14fb-25a3-49fa-aeca-f7af72a58842", "User", "USER" },
                    { "bb9aea7e-2855-4d2a-b176-39f892109c65", "0175fcf9-bec0-4fc1-82af-7b90e7e4269d", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "208d39b1-2fb2-4f85-a352-5142f255e3b9", "AQAAAAIAAYagAAAAEBrgL17p65B73t4wwG/4VJrfSA+IcCQMIAd2i8KWRQmN7s4C9DnQ5bAgOe5t5Bd0sg==", "1e97001a-ba9c-4c35-8b87-df9ef7993480" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 28, 17, 3, 3, 652, DateTimeKind.Local).AddTicks(1776));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 28, 17, 3, 3, 652, DateTimeKind.Local).AddTicks(1832));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 28, 17, 3, 3, 652, DateTimeKind.Local).AddTicks(1836));
        }
    }
}
