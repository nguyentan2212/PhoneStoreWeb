using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneStoreWeb.Data.Migrations
{
    public partial class fixDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductUrl",
                table: "ProductLanguages");

            migrationBuilder.DropColumn(
                name: "CategoryUrl",
                table: "CategoryLanguages");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created_At",
                value: new DateTime(2020, 11, 16, 11, 15, 7, 858, DateTimeKind.Local).AddTicks(3967));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductUrl",
                table: "ProductLanguages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryUrl",
                table: "CategoryLanguages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created_At",
                value: new DateTime(2020, 11, 15, 22, 44, 38, 707, DateTimeKind.Local).AddTicks(2173));

            migrationBuilder.UpdateData(
                table: "CategoryLanguages",
                keyColumns: new[] { "CategoryId", "LanguageId" },
                keyValues: new object[] { 1, "en" },
                column: "CategoryUrl",
                value: "cake");

            migrationBuilder.UpdateData(
                table: "CategoryLanguages",
                keyColumns: new[] { "CategoryId", "LanguageId" },
                keyValues: new object[] { 1, "vn" },
                column: "CategoryUrl",
                value: "banh-ngot");

            migrationBuilder.UpdateData(
                table: "ProductLanguages",
                keyColumns: new[] { "LanguageId", "ProductId" },
                keyValues: new object[] { "en", 1 },
                column: "ProductUrl",
                value: "cake1");

            migrationBuilder.UpdateData(
                table: "ProductLanguages",
                keyColumns: new[] { "LanguageId", "ProductId" },
                keyValues: new object[] { "vn", 1 },
                column: "ProductUrl",
                value: "banh-ngot1");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "ec99be3a-50d9-4225-8b28-aaedb69840fe");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dd"),
                column: "ConcurrencyStamp",
                value: "48ef0fc2-84ed-4906-86f6-3aeaa388dfba");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b039473a-640e-495e-bc95-e474323499ba", "AQAAAAEAACcQAAAAEEVWDjx3s921r2V3rKOYSRFMLD80Hpsvanj2Rsn+h73YI7XXxqmKrbNecCMkGwdmIw==" });
        }
    }
}
