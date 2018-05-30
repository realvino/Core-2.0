using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlternewCompanyTableAddDesig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DesignationId",
                table: "NewContact",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewContact_DesignationId",
                table: "NewContact",
                column: "DesignationId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewContact_Designations_DesignationId",
                table: "NewContact",
                column: "DesignationId",
                principalTable: "Designations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewContact_Designations_DesignationId",
                table: "NewContact");

            migrationBuilder.DropIndex(
                name: "IX_NewContact_DesignationId",
                table: "NewContact");

            migrationBuilder.DropColumn(
                name: "DesignationId",
                table: "NewContact");
        }
    }
}
