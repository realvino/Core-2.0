using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterinquiryadddatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OpportunitySourceId",
                table: "Inquiry",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WhyBafcoId",
                table: "Inquiry",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_OpportunitySourceId",
                table: "Inquiry",
                column: "OpportunitySourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_WhyBafcoId",
                table: "Inquiry",
                column: "WhyBafcoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_OpportunitySource_OpportunitySourceId",
                table: "Inquiry",
                column: "OpportunitySourceId",
                principalTable: "OpportunitySource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_Ybafco_WhyBafcoId",
                table: "Inquiry",
                column: "WhyBafcoId",
                principalTable: "Ybafco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_OpportunitySource_OpportunitySourceId",
                table: "Inquiry");

            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_Ybafco_WhyBafcoId",
                table: "Inquiry");

            migrationBuilder.DropIndex(
                name: "IX_Inquiry_OpportunitySourceId",
                table: "Inquiry");

            migrationBuilder.DropIndex(
                name: "IX_Inquiry_WhyBafcoId",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "OpportunitySourceId",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "WhyBafcoId",
                table: "Inquiry");
        }
    }
}
