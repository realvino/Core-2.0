using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterQuotationProductTableMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OverAllDiscount",
                table: "QuotationProduct",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OverAllPrice",
                table: "QuotationProduct",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TemporaryCode",
                table: "QuotationProduct",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OverAllDiscount",
                table: "QuotationProduct");

            migrationBuilder.DropColumn(
                name: "OverAllPrice",
                table: "QuotationProduct");

            migrationBuilder.DropColumn(
                name: "TemporaryCode",
                table: "QuotationProduct");
        }
    }
}
