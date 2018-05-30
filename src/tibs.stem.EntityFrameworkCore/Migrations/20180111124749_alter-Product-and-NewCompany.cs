using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterProductandNewCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Depth",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ApprovedById",
                table: "NewCompany",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "NewCompany",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_NewCompany_ApprovedById",
                table: "NewCompany",
                column: "ApprovedById");

            migrationBuilder.AddForeignKey(
                name: "FK_NewCompany_AbpUsers_ApprovedById",
                table: "NewCompany",
                column: "ApprovedById",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewCompany_AbpUsers_ApprovedById",
                table: "NewCompany");

            migrationBuilder.DropIndex(
                name: "IX_NewCompany_ApprovedById",
                table: "NewCompany");

            migrationBuilder.DropColumn(
                name: "Depth",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "NewCompany");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "NewCompany");
        }
    }
}
