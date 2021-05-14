using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class ModifiedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaderResponseId",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Ideas");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Attachments",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Attachments",
                newName: "FileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Attachments",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Attachments",
                newName: "Description");

            migrationBuilder.AddColumn<Guid>(
                name: "LeaderResponseId",
                table: "Ideas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Ideas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
