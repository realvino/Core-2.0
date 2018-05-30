using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterQuotationTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_AbpUsers_AttentionId",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_AttentionId",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "AttentionId",
                table: "Quotation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AttentionId",
                table: "Quotation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_AttentionId",
                table: "Quotation",
                column: "AttentionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_AbpUsers_AttentionId",
                table: "Quotation",
                column: "AttentionId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
