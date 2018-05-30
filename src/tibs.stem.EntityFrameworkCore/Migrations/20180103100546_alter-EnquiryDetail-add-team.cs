using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterEnquiryDetailaddteam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "EnquiryDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnquiryDetail_TeamId",
                table: "EnquiryDetail",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnquiryDetail_Teams_TeamId",
                table: "EnquiryDetail",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnquiryDetail_Teams_TeamId",
                table: "EnquiryDetail");

            migrationBuilder.DropIndex(
                name: "IX_EnquiryDetail_TeamId",
                table: "EnquiryDetail");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "EnquiryDetail");
        }
    }
}
