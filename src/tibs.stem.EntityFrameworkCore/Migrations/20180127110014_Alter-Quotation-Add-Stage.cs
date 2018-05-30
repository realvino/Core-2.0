using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterQuotationAddStage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StageId",
                table: "Quotation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_StageId",
                table: "Quotation",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_EnquiryStatus_StageId",
                table: "Quotation",
                column: "StageId",
                principalTable: "EnquiryStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_EnquiryStatus_StageId",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_StageId",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "Quotation");
        }
    }
}
