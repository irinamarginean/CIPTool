using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class AddedLeaderResponseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "LeaderResponseDate",
                table: "Ideas");

            migrationBuilder.AddColumn<Guid>(
                name: "LeaderResponseId",
                table: "Ideas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LeaderResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaderResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdeaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaderResponses_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaderResponses_IdeaId",
                table: "LeaderResponses",
                column: "IdeaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaderResponses");

            migrationBuilder.DropColumn(
                name: "LeaderResponseId",
                table: "Ideas");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Ideas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LeaderResponseDate",
                table: "Ideas",
                type: "datetime2",
                nullable: true);
        }
    }
}
