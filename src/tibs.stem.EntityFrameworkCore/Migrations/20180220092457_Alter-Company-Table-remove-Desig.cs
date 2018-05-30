using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterCompanyTableremoveDesig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewContact_Industry_IndustryId",
                table: "NewContact");

            migrationBuilder.DropIndex(
                name: "IX_NewContact_IndustryId",
                table: "NewContact");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "NewContact");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IndustryId",
                table: "NewContact",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewContact_IndustryId",
                table: "NewContact",
                column: "IndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewContact_Industry_IndustryId",
                table: "NewContact",
                column: "IndustryId",
                principalTable: "Industry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
