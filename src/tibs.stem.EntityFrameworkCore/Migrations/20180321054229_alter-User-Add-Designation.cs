using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterUserAddDesignation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserDesignationId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_UserDesignationId",
                table: "AbpUsers",
                column: "UserDesignationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_UserDesignation_UserDesignationId",
                table: "AbpUsers",
                column: "UserDesignationId",
                principalTable: "UserDesignation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_UserDesignation_UserDesignationId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_UserDesignationId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "UserDesignationId",
                table: "AbpUsers");
        }
    }
}
