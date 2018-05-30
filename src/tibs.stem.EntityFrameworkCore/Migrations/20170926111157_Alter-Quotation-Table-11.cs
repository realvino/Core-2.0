using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterQuotationTable11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "Quotation",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Quotation");
        }
    }
}
