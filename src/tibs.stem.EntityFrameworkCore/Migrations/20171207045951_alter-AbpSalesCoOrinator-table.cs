using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterAbpSalesCoOrinatortable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "AbpSalesCoOrinator",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpSalesCoOrinator_UserId",
                table: "AbpSalesCoOrinator",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpSalesCoOrinator_AbpUsers_UserId",
                table: "AbpSalesCoOrinator",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpSalesCoOrinator_AbpUsers_UserId",
                table: "AbpSalesCoOrinator");

            migrationBuilder.DropIndex(
                name: "IX_AbpSalesCoOrinator_UserId",
                table: "AbpSalesCoOrinator");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AbpSalesCoOrinator");
        }
    }
}
