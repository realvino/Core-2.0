using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterQuotationAddApproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Quotation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Negotiation",
                table: "Quotation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "NegotiationDate",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OverAllDiscountAmount",
                table: "Quotation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OverAllDiscountPercentage",
                table: "Quotation",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "Negotiation",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "NegotiationDate",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "OverAllDiscountAmount",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "OverAllDiscountPercentage",
                table: "Quotation");
        }
    }
}
