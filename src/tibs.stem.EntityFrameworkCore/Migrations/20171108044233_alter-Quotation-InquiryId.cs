using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterQuotationInquiryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InquiryId",
                table: "Quotation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_InquiryId",
                table: "Quotation",
                column: "InquiryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_Inquiry_InquiryId",
                table: "Quotation",
                column: "InquiryId",
                principalTable: "Inquiry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_Inquiry_InquiryId",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_InquiryId",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "InquiryId",
                table: "Quotation");
        }
    }
}
