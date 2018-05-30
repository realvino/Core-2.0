using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterCompanyTableaddIndustry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IndustryId",
                table: "NewCompany",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewCompany_IndustryId",
                table: "NewCompany",
                column: "IndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewCompany_Industry_IndustryId",
                table: "NewCompany",
                column: "IndustryId",
                principalTable: "Industry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewCompany_Industry_IndustryId",
                table: "NewCompany");

            migrationBuilder.DropIndex(
                name: "IX_NewCompany_IndustryId",
                table: "NewCompany");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "NewCompany");
        }
    }
}
