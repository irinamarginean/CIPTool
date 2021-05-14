using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class AddedPDCADatesInIdeaEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubmittedAt",
                table: "Ideas",
                newName: "PlanDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActDate",
                table: "Ideas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckDate",
                table: "Ideas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DoDate",
                table: "Ideas",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActDate",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "CheckDate",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "DoDate",
                table: "Ideas");

            migrationBuilder.RenameColumn(
                name: "PlanDate",
                table: "Ideas",
                newName: "SubmittedAt");
        }
    }
}
