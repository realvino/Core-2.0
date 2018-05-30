using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterQuotationAddRFQ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RFQNo",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefQNo",
                table: "Quotation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RFQNo",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "RefQNo",
                table: "Quotation");
        }
    }
}
