using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterProductGroupDetails1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttributeGroupId",
                table: "ProductSpecificationDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationDetail_AttributeGroupId",
                table: "ProductSpecificationDetail",
                column: "AttributeGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecificationDetail_AttributeGroup_AttributeGroupId",
                table: "ProductSpecificationDetail",
                column: "AttributeGroupId",
                principalTable: "AttributeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecificationDetail_AttributeGroup_AttributeGroupId",
                table: "ProductSpecificationDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProductSpecificationDetail_AttributeGroupId",
                table: "ProductSpecificationDetail");

            migrationBuilder.DropColumn(
                name: "AttributeGroupId",
                table: "ProductSpecificationDetail");
        }
    }
}
