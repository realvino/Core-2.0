using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class altertableaddSomeColumsViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ActionDateFilter",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationDateFilter",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PercentageFilter",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StageFilter",
                table: "View",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionDateFilter",
                table: "View");

            migrationBuilder.DropColumn(
                name: "CreationDateFilter",
                table: "View");

            migrationBuilder.DropColumn(
                name: "PercentageFilter",
                table: "View");

            migrationBuilder.DropColumn(
                name: "StageFilter",
                table: "View");

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
    }
}
