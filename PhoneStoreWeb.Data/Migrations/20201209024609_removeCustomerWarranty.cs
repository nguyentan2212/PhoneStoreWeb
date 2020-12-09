using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneStoreWeb.Data.Migrations
{
    public partial class removeCustomerWarranty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyCards_AspNetUsers_CustomerID",
                table: "WarrantyCards");

            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyCards_AspNetUsers_StaffID",
                table: "WarrantyCards");

            migrationBuilder.DropIndex(
                name: "IX_WarrantyCards_CustomerID",
                table: "WarrantyCards");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "WarrantyCards");

            migrationBuilder.DropColumn(
                name: "ShipEmail",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "StaffID",
                table: "WarrantyCards",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_WarrantyCards_StaffID",
                table: "WarrantyCards",
                newName: "IX_WarrantyCards_AppUserId");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Orders",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ShipPhoneNumber",
                table: "Orders",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "ShipName",
                table: "Orders",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ShipAddress",
                table: "Orders",
                newName: "Address");

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyCards_AspNetUsers_AppUserId",
                table: "WarrantyCards",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyCards_AspNetUsers_AppUserId",
                table: "WarrantyCards");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "WarrantyCards",
                newName: "StaffID");

            migrationBuilder.RenameIndex(
                name: "IX_WarrantyCards_AppUserId",
                table: "WarrantyCards",
                newName: "IX_WarrantyCards_StaffID");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Orders",
                newName: "ShipPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Orders",
                newName: "ShipName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Orders",
                newName: "TransactionId");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Orders",
                newName: "ShipAddress");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerID",
                table: "WarrantyCards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ShipEmail",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyCards_CustomerID",
                table: "WarrantyCards",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyCards_AspNetUsers_CustomerID",
                table: "WarrantyCards",
                column: "CustomerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyCards_AspNetUsers_StaffID",
                table: "WarrantyCards",
                column: "StaffID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
