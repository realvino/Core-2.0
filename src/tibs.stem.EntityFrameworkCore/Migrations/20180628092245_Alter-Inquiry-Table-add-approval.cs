using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterInquiryTableaddapproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DesignerApproval",
                table: "Inquiry",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RevisionApproval",
                table: "Inquiry",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Stared",
                table: "Inquiry",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Weightedvalue",
                table: "Inquiry",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesignerApproval",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "RevisionApproval",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "Stared",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "Weightedvalue",
                table: "Inquiry");
        }
    }
}
