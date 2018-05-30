using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tibs.stem.Migrations
{
    public partial class addQuotationstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quotation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    NewCompanyId = table.Column<int>(nullable: true),
                    QuotationStatusId = table.Column<int>(nullable: false),
                    RefNo = table.Column<string>(nullable: true),
                    SalesPersonId = table.Column<long>(nullable: true),
                    TermsandCondition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotation_NewCompany_NewCompanyId",
                        column: x => x.NewCompanyId,
                        principalTable: "NewCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_QuotationStatus_QuotationStatusId",
                        column: x => x.QuotationStatusId,
                        principalTable: "QuotationStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quotation_AbpUsers_SalesPersonId",
                        column: x => x.SalesPersonId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_NewCompanyId",
                table: "Quotation",
                column: "NewCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_QuotationStatusId",
                table: "Quotation",
                column: "QuotationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_SalesPersonId",
                table: "Quotation",
                column: "SalesPersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotation");
        }
    }
}
