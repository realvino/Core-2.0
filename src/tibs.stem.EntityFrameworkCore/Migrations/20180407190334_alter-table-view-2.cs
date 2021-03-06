﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class altertableview2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DateFilterId",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnquiry",
                table: "View",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_View_DateFilterId",
                table: "View",
                column: "DateFilterId");

            migrationBuilder.AddForeignKey(
                name: "FK_View_DateFilter_DateFilterId",
                table: "View",
                column: "DateFilterId",
                principalTable: "DateFilter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_View_DateFilter_DateFilterId",
                table: "View");

            migrationBuilder.DropIndex(
                name: "IX_View_DateFilterId",
                table: "View");

            migrationBuilder.DropColumn(
                name: "DateFilterId",
                table: "View");

            migrationBuilder.DropColumn(
                name: "IsEnquiry",
                table: "View");
        }
    }
}
