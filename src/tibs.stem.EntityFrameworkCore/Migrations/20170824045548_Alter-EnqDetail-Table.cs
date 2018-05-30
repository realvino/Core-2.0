using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterEnqDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompatitorsId",
                table: "EnquiryDetail",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimationValue",
                table: "EnquiryDetail",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "LeadTypeId",
                table: "EnquiryDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "EnquiryDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "EnquiryDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnquiryDetail_CompatitorsId",
                table: "EnquiryDetail",
                column: "CompatitorsId");

            migrationBuilder.CreateIndex(
                name: "IX_EnquiryDetail_LeadTypeId",
                table: "EnquiryDetail",
                column: "LeadTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnquiryDetail_NewCompany_CompatitorsId",
                table: "EnquiryDetail",
                column: "CompatitorsId",
                principalTable: "NewCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EnquiryDetail_LeadType_LeadTypeId",
                table: "EnquiryDetail",
                column: "LeadTypeId",
                principalTable: "LeadType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnquiryDetail_NewCompany_CompatitorsId",
                table: "EnquiryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EnquiryDetail_LeadType_LeadTypeId",
                table: "EnquiryDetail");

            migrationBuilder.DropIndex(
                name: "IX_EnquiryDetail_CompatitorsId",
                table: "EnquiryDetail");

            migrationBuilder.DropIndex(
                name: "IX_EnquiryDetail_LeadTypeId",
                table: "EnquiryDetail");

            migrationBuilder.DropColumn(
                name: "CompatitorsId",
                table: "EnquiryDetail");

            migrationBuilder.DropColumn(
                name: "EstimationValue",
                table: "EnquiryDetail");

            migrationBuilder.DropColumn(
                name: "LeadTypeId",
                table: "EnquiryDetail");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "EnquiryDetail");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "EnquiryDetail");
        }
    }
}
