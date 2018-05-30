using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterQuotationTableReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompatitorId",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PONumber",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReasonId",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonRemark",
                table: "Quotation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_CompatitorId",
                table: "Quotation",
                column: "CompatitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_ReasonId",
                table: "Quotation",
                column: "ReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_NewCompany_CompatitorId",
                table: "Quotation",
                column: "CompatitorId",
                principalTable: "NewCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_LeadReason_ReasonId",
                table: "Quotation",
                column: "ReasonId",
                principalTable: "LeadReason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_NewCompany_CompatitorId",
                table: "Quotation");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_LeadReason_ReasonId",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_CompatitorId",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_ReasonId",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "CompatitorId",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "PONumber",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "ReasonId",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "ReasonRemark",
                table: "Quotation");
        }
    }
}
