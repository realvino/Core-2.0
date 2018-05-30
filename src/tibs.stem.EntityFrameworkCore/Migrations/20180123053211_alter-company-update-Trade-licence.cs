using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class altercompanyupdateTradelicence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TRNnumber",
                table: "NewCompany",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeLicense",
                table: "NewCompany",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TRNnumber",
                table: "NewCompany");

            migrationBuilder.DropColumn(
                name: "TradeLicense",
                table: "NewCompany");
        }
    }
}
