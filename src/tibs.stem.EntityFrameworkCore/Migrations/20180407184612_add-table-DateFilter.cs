using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class addtableDateFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AllPersonId",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GraterAmount",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessAmount",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuotationStatusId",
                table: "View",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_View_AllPersonId",
                table: "View",
                column: "AllPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_View_QuotationStatusId",
                table: "View",
                column: "QuotationStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_View_AbpUsers_AllPersonId",
                table: "View",
                column: "AllPersonId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_View_QuotationStatus_QuotationStatusId",
                table: "View",
                column: "QuotationStatusId",
                principalTable: "QuotationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_View_AbpUsers_AllPersonId",
                table: "View");

            migrationBuilder.DropForeignKey(
                name: "FK_View_QuotationStatus_QuotationStatusId",
                table: "View");

            migrationBuilder.DropIndex(
                name: "IX_View_AllPersonId",
                table: "View");

            migrationBuilder.DropIndex(
                name: "IX_View_QuotationStatusId",
                table: "View");

            migrationBuilder.DropColumn(
                name: "AllPersonId",
                table: "View");

            migrationBuilder.DropColumn(
                name: "GraterAmount",
                table: "View");

            migrationBuilder.DropColumn(
                name: "LessAmount",
                table: "View");

            migrationBuilder.DropColumn(
                name: "QuotationStatusId",
                table: "View");
        }
    }
}
