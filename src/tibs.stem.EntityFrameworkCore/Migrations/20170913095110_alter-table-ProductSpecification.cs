using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class altertableProductSpecification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FamilyId",
                table: "ProductSpecification",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecification_FamilyId",
                table: "ProductSpecification",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecification_ProductFamily_FamilyId",
                table: "ProductSpecification",
                column: "FamilyId",
                principalTable: "ProductFamily",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecification_ProductFamily_FamilyId",
                table: "ProductSpecification");

            migrationBuilder.DropIndex(
                name: "IX_ProductSpecification_FamilyId",
                table: "ProductSpecification");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "ProductSpecification");
        }
    }
}
