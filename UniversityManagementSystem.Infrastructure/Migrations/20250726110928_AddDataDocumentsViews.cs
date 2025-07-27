using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDataDocumentsViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "DocumentTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HtmlContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TemplateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IssuedTo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DocumentNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SignatureData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FieldValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FieldType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentFields_OfficialDocuments_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "OfficialDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentSignatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SignerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SignerPosition = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SignatureData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignatureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SignatureType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSignatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentSignatures_OfficialDocuments_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "OfficialDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a277451c-c100-4b06-9cb0-c5af9b237459", "73974207-95e0-413a-a2e9-e4d4e09ef1a6", "User", "USER" },
                    { "b993ff96-f04d-43ef-8068-f3a848780b88", "4ce605d0-b91f-4a77-8e8d-e4d449334586", "Student", "STUDENT" },
                    { "edbcd87c-41c7-4432-a159-d8bf2a5373e6", "0b1c490e-23b9-462d-91db-377e4ca5520b", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51586e47-b125-4534-bba4-9bc6fd3dfbc8",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6715dea9-edeb-4c19-99aa-814ae7babc71", new DateTime(2025, 7, 26, 11, 9, 20, 757, DateTimeKind.Utc).AddTicks(5190), "AQAAAAIAAYagAAAAENwIqz9N1B/PrFNztsueDznSIRqH5XywKT1j0Uu+vR80+ihEGHGuE3g2fXYeUIZLMg==", "56366ea6-38b2-48cf-8c11-c61d31903aaa" });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFields_DocumentId",
                table: "DocumentFields",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSignatures_DocumentId",
                table: "DocumentSignatures",
                column: "DocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentFields");

            migrationBuilder.DropTable(
                name: "DocumentSignatures");

            migrationBuilder.DropTable(
                name: "DocumentTemplates");

            migrationBuilder.DropTable(
                name: "OfficialDocuments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a277451c-c100-4b06-9cb0-c5af9b237459");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b993ff96-f04d-43ef-8068-f3a848780b88");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edbcd87c-41c7-4432-a159-d8bf2a5373e6");

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
        }
    }
}
