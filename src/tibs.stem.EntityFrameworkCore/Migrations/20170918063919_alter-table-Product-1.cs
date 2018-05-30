using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class altertableProduct1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gpcode",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuspectCode",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Gpcode",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SuspectCode",
                table: "Product");
        }
    }
}
