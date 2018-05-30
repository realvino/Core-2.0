using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterProductGroupDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecificationDetail_ProductGroup_ProductGroupId",
                table: "ProductSpecificationDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProductSpecificationDetail_ProductGroupId",
                table: "ProductSpecificationDetail");

            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                table: "ProductSpecificationDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductGroupId",
                table: "ProductSpecificationDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationDetail_ProductGroupId",
                table: "ProductSpecificationDetail",
                column: "ProductGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecificationDetail_ProductGroup_ProductGroupId",
                table: "ProductSpecificationDetail",
                column: "ProductGroupId",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
