using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterviewsomecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StageFilter",
                table: "View",
                newName: "WhyBafco");

            migrationBuilder.RenameColumn(
                name: "PercentageFilter",
                table: "View",
                newName: "UserIds");

            migrationBuilder.RenameColumn(
                name: "CreationDateFilter",
                table: "View",
                newName: "TeamName");

            migrationBuilder.RenameColumn(
                name: "ActionDateFilter",
                table: "View",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Categories",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClosureDate",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Coordinator",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepatmentName",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesignationName",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designer",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Emirates",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnquiryStatus",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InquiryCreateBy",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InquiryCreation",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastActivity",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MileStoneName",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PotentialCustomer",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Probability",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuotationCreateBy",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuotationCreation",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuotationStatus",
                table: "View",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Salesman",
                table: "View",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categories",
                table: "View");

            migrationBuilder.DropColumn(
                name: "ClosureDate",
                table: "View");

            migrationBuilder.DropColumn(
                name: "Coordinator",
                table: "View");

            migrationBuilder.DropColumn(
                name: "DepatmentName",
                table: "View");

            migrationBuilder.DropColumn(
                name: "DesignationName",
                table: "View");

            migrationBuilder.DropColumn(
                name: "Designer",
                table: "View");

            migrationBuilder.DropColumn(
                name: "Emirates",
                table: "View");

            migrationBuilder.DropColumn(
                name: "EnquiryStatus",
                table: "View");

            migrationBuilder.DropColumn(
                name: "InquiryCreateBy",
                table: "View");

            migrationBuilder.DropColumn(
                name: "InquiryCreation",
                table: "View");

            migrationBuilder.DropColumn(
                name: "LastActivity",
                table: "View");

            migrationBuilder.DropColumn(
                name: "MileStoneName",
                table: "View");

            migrationBuilder.DropColumn(
                name: "PotentialCustomer",
                table: "View");

            migrationBuilder.DropColumn(
                name: "Probability",
                table: "View");

            migrationBuilder.DropColumn(
                name: "QuotationCreateBy",
                table: "View");

            migrationBuilder.DropColumn(
                name: "QuotationCreation",
                table: "View");

            migrationBuilder.DropColumn(
                name: "QuotationStatus",
                table: "View");

            migrationBuilder.DropColumn(
                name: "Salesman",
                table: "View");

            migrationBuilder.RenameColumn(
                name: "WhyBafco",
                table: "View",
                newName: "StageFilter");

            migrationBuilder.RenameColumn(
                name: "UserIds",
                table: "View",
                newName: "PercentageFilter");

            migrationBuilder.RenameColumn(
                name: "TeamName",
                table: "View",
                newName: "CreationDateFilter");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "View",
                newName: "ActionDateFilter");
        }
    }
}
