using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterInquiryandQuotationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archieved",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Archieved",
                table: "Inquiry",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Inquiry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archieved",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "Archieved",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Inquiry");
        }
    }
}
