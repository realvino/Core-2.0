using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterProductspec2Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductGroupId",
                table: "ProductSpecification",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecification_ProductGroupId",
                table: "ProductSpecification",
                column: "ProductGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecification_ProductGroup_ProductGroupId",
                table: "ProductSpecification",
                column: "ProductGroupId",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecification_ProductGroup_ProductGroupId",
                table: "ProductSpecification");

            migrationBuilder.DropIndex(
                name: "IX_ProductSpecification_ProductGroupId",
                table: "ProductSpecification");

            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                table: "ProductSpecification");
        }
    }
}
