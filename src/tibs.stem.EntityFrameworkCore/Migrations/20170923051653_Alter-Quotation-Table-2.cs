using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterQuotationTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttentionContactId",
                table: "Quotation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_AttentionContactId",
                table: "Quotation",
                column: "AttentionContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_NewContact_AttentionContactId",
                table: "Quotation",
                column: "AttentionContactId",
                principalTable: "NewContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_NewContact_AttentionContactId",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_AttentionContactId",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "AttentionContactId",
                table: "Quotation");
        }
    }
}
