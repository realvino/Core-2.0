using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterProductGroupaddcatagiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "ProductGroup",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_ProductCategoryId",
                table: "ProductGroup",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroup_ProductCategory_ProductCategoryId",
                table: "ProductGroup",
                column: "ProductCategoryId",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroup_ProductCategory_ProductCategoryId",
                table: "ProductGroup");

            migrationBuilder.DropIndex(
                name: "IX_ProductGroup_ProductCategoryId",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "ProductGroup");
        }
    }
}
