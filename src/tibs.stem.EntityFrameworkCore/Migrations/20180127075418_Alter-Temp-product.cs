using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterTempproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Depth",
                table: "TemporaryProduct",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "TemporaryProduct",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "TemporaryProduct",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Depth",
                table: "TemporaryProduct");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "TemporaryProduct");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "TemporaryProduct");
        }
    }
}
