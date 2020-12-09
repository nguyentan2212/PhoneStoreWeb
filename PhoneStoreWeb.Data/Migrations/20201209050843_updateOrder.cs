using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneStoreWeb.Data.Migrations
{
    public partial class updateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "FinalPrice",
                table: "Orders",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "Total");
        }
    }
}
