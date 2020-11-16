using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneStoreWeb.Data.Migrations
{
    public partial class deleteLanguageClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryLanguages");

            migrationBuilder.DropTable(
                name: "ProductLanguages");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created_At",
                value: new DateTime(2020, 11, 16, 17, 1, 58, 653, DateTimeKind.Local).AddTicks(9792));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "cf725855-5205-44b4-95b3-14d26efb2e6a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dd"),
                column: "ConcurrencyStamp",
                value: "ce3da197-33fd-43e8-bfa6-7b7cd05d52cf");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "43a12b2e-619f-4491-8ccd-d87f8dcad3bc", "AQAAAAEAACcQAAAAEGcnzFw41cwFK4blJvmkcMYJUGtRWWkh/YSBIu7YISw8SLD6mrCwWVz8ChXqMMMQ5w==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryLanguages",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLanguages", x => new { x.CategoryId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_CategoryLanguages_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductLanguages",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLanguages", x => new { x.ProductId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_ProductLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductLanguages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created_At",
                value: new DateTime(2020, 11, 16, 11, 15, 7, 858, DateTimeKind.Local).AddTicks(3967));

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IsDefault", "Name" },
                values: new object[,]
                {
                    { "vn", false, "VIETNAM" },
                    { "en", false, "ENGLISH" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "c1c2e6be-9cb3-4564-bbd9-48dd7a65ef00");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dd"),
                column: "ConcurrencyStamp",
                value: "5805df75-7899-4437-b17b-e60f61718716");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e654f6f5-93af-4afe-b3ee-a91fba22393b", "AQAAAAEAACcQAAAAEB4pQJbfDBCVOnHVc1SAyJAMQZb3Oqtg6Phg3eEw+X6bzFJbohwDxTDZO9j+r3ZWaQ==" });

            migrationBuilder.InsertData(
                table: "CategoryLanguages",
                columns: new[] { "CategoryId", "LanguageId", "Name" },
                values: new object[,]
                {
                    { 1, "vn", "Bánh ngọt" },
                    { 1, "en", "Cake" }
                });

            migrationBuilder.InsertData(
                table: "ProductLanguages",
                columns: new[] { "LanguageId", "ProductId", "Description", "Name" },
                values: new object[,]
                {
                    { "vn", 1, "This is banh ngot 1", "Bánh ngọt1" },
                    { "en", 1, "This is cake1", "Cake1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLanguages_LanguageId",
                table: "CategoryLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLanguages_LanguageId",
                table: "ProductLanguages",
                column: "LanguageId");
        }
    }
}
