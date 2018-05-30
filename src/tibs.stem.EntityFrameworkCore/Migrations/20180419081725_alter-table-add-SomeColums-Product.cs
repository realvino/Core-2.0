using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class altertableaddSomeColumsProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionDateFilter",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationDateFilter",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PercentageFilter",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StageFilter",
                table: "Quotation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionDateFilter",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "CreationDateFilter",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "PercentageFilter",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "StageFilter",
                table: "Quotation");
        }
    }
}
