using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class addwonEnquiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DisableQuotation",
                table: "Inquiry",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "Inquiry",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Won",
                table: "Inquiry",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisableQuotation",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "Won",
                table: "Inquiry");
        }
    }
}
