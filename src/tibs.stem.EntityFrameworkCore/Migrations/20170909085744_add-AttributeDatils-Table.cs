using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tibs.stem.Migrations
{
    public partial class addAttributeDatilsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeGroup_ProductAttributes_AttributeId",
                table: "AttributeGroup");

            migrationBuilder.DropIndex(
                name: "IX_AttributeGroup_AttributeId",
                table: "AttributeGroup");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "AttributeGroup");

            migrationBuilder.CreateTable(
                name: "AttributeGroupDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttributeGroupId = table.Column<int>(nullable: false),
                    AttributeId = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeGroupDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeGroupDetail_AttributeGroup_AttributeGroupId",
                        column: x => x.AttributeGroupId,
                        principalTable: "AttributeGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeGroupDetail_ProductAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeGroupDetail_AttributeGroupId",
                table: "AttributeGroupDetail",
                column: "AttributeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeGroupDetail_AttributeId",
                table: "AttributeGroupDetail",
                column: "AttributeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeGroupDetail");

            migrationBuilder.AddColumn<int>(
                name: "AttributeId",
                table: "AttributeGroup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeGroup_AttributeId",
                table: "AttributeGroup",
                column: "AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeGroup_ProductAttributes_AttributeId",
                table: "AttributeGroup",
                column: "AttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
