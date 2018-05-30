using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterinquiryadddatasss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Inquiry",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_LocationId",
                table: "Inquiry",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_Location_LocationId",
                table: "Inquiry",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_Location_LocationId",
                table: "Inquiry");

            migrationBuilder.DropIndex(
                name: "IX_Inquiry_LocationId",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Inquiry");
        }
    }
}
