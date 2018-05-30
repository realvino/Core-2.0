using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterProductGroupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductGroupCode",
                table: "ProductGroup");

            migrationBuilder.AddColumn<string>(
                name: "AttributeData",
                table: "ProductGroup",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FamilyId",
                table: "ProductGroup",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_FamilyId",
                table: "ProductGroup",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroup_ProductFamily_FamilyId",
                table: "ProductGroup",
                column: "FamilyId",
                principalTable: "ProductFamily",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroup_ProductFamily_FamilyId",
                table: "ProductGroup");

            migrationBuilder.DropIndex(
                name: "IX_ProductGroup_FamilyId",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "AttributeData",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "ProductGroup");

            migrationBuilder.AddColumn<string>(
                name: "ProductGroupCode",
                table: "ProductGroup",
                nullable: false,
                defaultValue: "");
        }
    }
}
