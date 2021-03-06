﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterleaddetailAddSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeadSourceId",
                table: "LeadDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeadDetails_LeadSourceId",
                table: "LeadDetails",
                column: "LeadSourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadDetails_OpportunitySource_LeadSourceId",
                table: "LeadDetails",
                column: "LeadSourceId",
                principalTable: "OpportunitySource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadDetails_OpportunitySource_LeadSourceId",
                table: "LeadDetails");

            migrationBuilder.DropIndex(
                name: "IX_LeadDetails_LeadSourceId",
                table: "LeadDetails");

            migrationBuilder.DropColumn(
                name: "LeadSourceId",
                table: "LeadDetails");
        }
    }
}
