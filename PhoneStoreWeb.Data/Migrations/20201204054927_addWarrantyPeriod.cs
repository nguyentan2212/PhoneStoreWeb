using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneStoreWeb.Data.Migrations
{
    public partial class addWarrantyPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarrantyPeriod",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WarrantyPeriod",
                table: "ProductItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarrantyPeriod",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WarrantyPeriod",
                table: "ProductItems");
        }
    }
}
