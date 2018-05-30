using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class altertableProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductSubGroup_ProductSubGroupId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductSubGroupId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Dimension",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Discontinue",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductSubGroupId",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dimension",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Discontinue",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductSubGroupId",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSubGroupId",
                table: "Product",
                column: "ProductSubGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductSubGroup_ProductSubGroupId",
                table: "Product",
                column: "ProductSubGroupId",
                principalTable: "ProductSubGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
