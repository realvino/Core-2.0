using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AddProductstatusinProducttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductStateId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductStateId",
                table: "Product",
                column: "ProductStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductStates_ProductStateId",
                table: "Product",
                column: "ProductStateId",
                principalTable: "ProductStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductStates_ProductStateId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductStateId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductStateId",
                table: "Product");
        }
    }
}
