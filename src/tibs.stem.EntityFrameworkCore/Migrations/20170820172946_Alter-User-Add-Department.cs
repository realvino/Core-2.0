using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterUserAddDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_DepartmentId",
                table: "AbpUsers",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Department_DepartmentId",
                table: "AbpUsers",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Department_DepartmentId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_DepartmentId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AbpUsers");
        }
    }
}
