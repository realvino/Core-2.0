using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class addDescriptionProductSpec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductSpecification",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductAttributes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductSpecification");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductAttributes");
        }
    }
}
