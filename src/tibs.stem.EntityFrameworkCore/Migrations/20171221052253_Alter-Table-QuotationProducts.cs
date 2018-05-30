using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterTableQuotationProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TemporaryProductId",
                table: "QuotationProduct",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuotationProduct_TemporaryProductId",
                table: "QuotationProduct",
                column: "TemporaryProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationProduct_TemporaryProduct_TemporaryProductId",
                table: "QuotationProduct",
                column: "TemporaryProductId",
                principalTable: "TemporaryProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationProduct_TemporaryProduct_TemporaryProductId",
                table: "QuotationProduct");

            migrationBuilder.DropIndex(
                name: "IX_QuotationProduct_TemporaryProductId",
                table: "QuotationProduct");

            migrationBuilder.DropColumn(
                name: "TemporaryProductId",
                table: "QuotationProduct");
        }
    }
}
