using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialOtherTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_University_UniversityId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentDocument_Students_StudentId",
                table: "StudentDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Tunnel_TunnelId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_University_Students_StudentId",
                table: "University");

            migrationBuilder.DropPrimaryKey(
                name: "PK_University",
                table: "University");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tunnel",
                table: "Tunnel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentDocument",
                table: "StudentDocument");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0df462c8-fb0a-486b-b977-df3e7e9a5a81");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "204d7c9c-fd80-46a9-88e9-82cfd39b89b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d88234a2-6ccd-4e64-8f1c-b0668c0628a8");

            migrationBuilder.RenameTable(
                name: "University",
                newName: "Universities");

            migrationBuilder.RenameTable(
                name: "Tunnel",
                newName: "Tunnels");

            migrationBuilder.RenameTable(
                name: "StudentDocument",
                newName: "StudentDocuments");

            migrationBuilder.RenameIndex(
                name: "IX_University_StudentId",
                table: "Universities",
                newName: "IX_Universities_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentDocument_StudentId",
                table: "StudentDocuments",
                newName: "IX_StudentDocuments_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Universities",
                table: "Universities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tunnels",
                table: "Tunnels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentDocuments",
                table: "StudentDocuments",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52429982-cd3a-4713-bd72-183142d117c9", "cd40c682-32e5-4656-919e-8e7ce744145f", "Employee", "EMPLOYEE" },
                    { "816b8801-f0d5-4440-bef0-f4d945b9ba9e", "1a7867b0-52be-4fa9-9b17-585dbfefdd75", "User", "USER" },
                    { "8243e92b-1e78-4748-857a-ecea00ea73bd", "49cc4658-432a-4a77-8941-3da7b063b1a6", "Student", "STUDENT" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60a0ca2a-c0c2-425c-81ab-a8e754cc7a57", new DateTime(2025, 7, 25, 7, 32, 30, 378, DateTimeKind.Utc).AddTicks(8811), "AQAAAAIAAYagAAAAEIR4Q5jZAJHy7Vy6L+Zhop1NhU5PZuKwkRhr2/uK2JbO7ShNDrnlwgDnOV3V/jFbNA==", "4032d4a6-1961-44a7-afcf-8ac62c3ac821" });

            migrationBuilder.InsertData(
                table: "Tunnels",
                columns: new[] { "Id", "FirstPart", "Name", "SecoundPart", "TypeOfkinship" },
                values: new object[,]
                {
                    { 1, "-", "NoTunnel", "-", "-" },
                    { 2, "-", "ابن شهيد", "-", "اب" }
                });

            migrationBuilder.InsertData(
                table: "Universities",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "DeletedBy", "Description", "ModifiedAt", "ModifiedBy", "Name", "StudentId", "Type" },
                values: new object[] { 2, new DateTime(2025, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Premier university in SVU", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SVU University", null, null });

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Universities_UniversityId",
                table: "Departments",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDocuments_Students_StudentId",
                table: "StudentDocuments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Tunnels_TunnelId",
                table: "Students",
                column: "TunnelId",
                principalTable: "Tunnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Universities_Students_StudentId",
                table: "Universities",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Universities_UniversityId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentDocuments_Students_StudentId",
                table: "StudentDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Tunnels_TunnelId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Universities_Students_StudentId",
                table: "Universities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Universities",
                table: "Universities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tunnels",
                table: "Tunnels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentDocuments",
                table: "StudentDocuments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52429982-cd3a-4713-bd72-183142d117c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "816b8801-f0d5-4440-bef0-f4d945b9ba9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8243e92b-1e78-4748-857a-ecea00ea73bd");

            migrationBuilder.DeleteData(
                table: "Tunnels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tunnels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Universities",
                newName: "University");

            migrationBuilder.RenameTable(
                name: "Tunnels",
                newName: "Tunnel");

            migrationBuilder.RenameTable(
                name: "StudentDocuments",
                newName: "StudentDocument");

            migrationBuilder.RenameIndex(
                name: "IX_Universities_StudentId",
                table: "University",
                newName: "IX_University_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentDocuments_StudentId",
                table: "StudentDocument",
                newName: "IX_StudentDocument_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_University",
                table: "University",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tunnel",
                table: "Tunnel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentDocument",
                table: "StudentDocument",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0df462c8-fb0a-486b-b977-df3e7e9a5a81", "331339d3-2c04-441b-8217-81005cd25c5e", "Employee", "EMPLOYEE" },
                    { "204d7c9c-fd80-46a9-88e9-82cfd39b89b1", "3a969597-6c40-4fa4-85e9-adeba6487377", "User", "USER" },
                    { "d88234a2-6ccd-4e64-8f1c-b0668c0628a8", "8353f785-fc77-4354-b194-ab8372b63794", "Student", "STUDENT" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12527e86-ffd3-48c2-9dc8-20f54383e59a", new DateTime(2025, 7, 25, 6, 38, 22, 529, DateTimeKind.Utc).AddTicks(3029), "AQAAAAIAAYagAAAAEOs4gqzdqpDmHMaRtS28wlG2QcWrYt3XYpd4C81YHTkEzI3N/57SuLwq03aai4dT6w==", "636afd09-ffbf-4e2f-8d2c-5d5b7119b076" });

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_University_UniversityId",
                table: "Departments",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDocument_Students_StudentId",
                table: "StudentDocument",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Tunnel_TunnelId",
                table: "Students",
                column: "TunnelId",
                principalTable: "Tunnel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_University_Students_StudentId",
                table: "University",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
