using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterEnqStatusAddStagestateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StagestateId",
                table: "EnquiryStatus",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnquiryStatus_StagestateId",
                table: "EnquiryStatus",
                column: "StagestateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnquiryStatus_Stagestate_StagestateId",
                table: "EnquiryStatus",
                column: "StagestateId",
                principalTable: "Stagestate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnquiryStatus_Stagestate_StagestateId",
                table: "EnquiryStatus");

            migrationBuilder.DropIndex(
                name: "IX_EnquiryStatus_StagestateId",
                table: "EnquiryStatus");

            migrationBuilder.DropColumn(
                name: "StagestateId",
                table: "EnquiryStatus");
        }
    }
}
