using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AltertableAddFilterIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClosureDateFilterId",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastActivityDateFilterId",
                table: "View",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_View_ClosureDateFilterId",
                table: "View",
                column: "ClosureDateFilterId");

            migrationBuilder.CreateIndex(
                name: "IX_View_LastActivityDateFilterId",
                table: "View",
                column: "LastActivityDateFilterId");

            migrationBuilder.AddForeignKey(
                name: "FK_View_DateFilter_ClosureDateFilterId",
                table: "View",
                column: "ClosureDateFilterId",
                principalTable: "DateFilter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_View_DateFilter_LastActivityDateFilterId",
                table: "View",
                column: "LastActivityDateFilterId",
                principalTable: "DateFilter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_View_DateFilter_ClosureDateFilterId",
                table: "View");

            migrationBuilder.DropForeignKey(
                name: "FK_View_DateFilter_LastActivityDateFilterId",
                table: "View");

            migrationBuilder.DropIndex(
                name: "IX_View_ClosureDateFilterId",
                table: "View");

            migrationBuilder.DropIndex(
                name: "IX_View_LastActivityDateFilterId",
                table: "View");

            migrationBuilder.DropColumn(
                name: "ClosureDateFilterId",
                table: "View");

            migrationBuilder.DropColumn(
                name: "LastActivityDateFilterId",
                table: "View");
        }
    }
}
