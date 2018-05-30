using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterproducttable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductSpecification_ProductSpecificationId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "ProductSpecificationId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductSpecification_ProductSpecificationId",
                table: "Product",
                column: "ProductSpecificationId",
                principalTable: "ProductSpecification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductSpecification_ProductSpecificationId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "ProductSpecificationId",
                table: "Product",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductSpecification_ProductSpecificationId",
                table: "Product",
                column: "ProductSpecificationId",
                principalTable: "ProductSpecification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
