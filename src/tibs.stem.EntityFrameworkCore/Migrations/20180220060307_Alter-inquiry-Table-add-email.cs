using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterinquiryTableaddemail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CEmail",
                table: "Inquiry",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CLandlineNumber",
                table: "Inquiry",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CMbNo",
                table: "Inquiry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEmail",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "CLandlineNumber",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "CMbNo",
                table: "Inquiry");
        }
    }
}
