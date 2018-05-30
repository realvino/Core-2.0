using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class altertableProducts1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductSpecificationId",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSpecificationId",
                table: "Product",
                column: "ProductSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductSpecification_ProductSpecificationId",
                table: "Product",
                column: "ProductSpecificationId",
                principalTable: "ProductSpecification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductSpecification_ProductSpecificationId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductSpecificationId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductSpecificationId",
                table: "Product");
        }
    }
}
