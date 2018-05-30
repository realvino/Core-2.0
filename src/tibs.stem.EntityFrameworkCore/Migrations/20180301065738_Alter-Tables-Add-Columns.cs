using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterTablesAddColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Discountable",
                table: "NewCompany",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnDiscountable",
                table: "NewCompany",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeadStatusId",
                table: "Inquiry",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_LeadStatusId",
                table: "Inquiry",
                column: "LeadStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_LeadStatus_LeadStatusId",
                table: "Inquiry",
                column: "LeadStatusId",
                principalTable: "LeadStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_LeadStatus_LeadStatusId",
                table: "Inquiry");

            migrationBuilder.DropIndex(
                name: "IX_Inquiry_LeadStatusId",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "Discountable",
                table: "NewCompany");

            migrationBuilder.DropColumn(
                name: "UnDiscountable",
                table: "NewCompany");

            migrationBuilder.DropColumn(
                name: "LeadStatusId",
                table: "Inquiry");
        }
    }
}
