using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class RemovedUnusedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaderResponses_AspNetUsers_ReviewerId",
                table: "LeaderResponses");

            migrationBuilder.DropTable(
                name: "IdeaStatuses");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_LeaderResponses_ReviewerId",
                table: "LeaderResponses");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewerId",
                table: "LeaderResponses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssociateId",
                table: "LeaderResponses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaderResponses_AssociateId",
                table: "LeaderResponses",
                column: "AssociateId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaderResponses_AspNetUsers_AssociateId",
                table: "LeaderResponses",
                column: "AssociateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaderResponses_AspNetUsers_AssociateId",
                table: "LeaderResponses");

            migrationBuilder.DropIndex(
                name: "IX_LeaderResponses_AssociateId",
                table: "LeaderResponses");

            migrationBuilder.DropColumn(
                name: "AssociateId",
                table: "LeaderResponses");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewerId",
                table: "LeaderResponses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdeaStatuses",
                columns: table => new
                {
                    IdeaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaStatuses", x => new { x.IdeaId, x.StatusId });
                    table.ForeignKey(
                        name: "FK_IdeaStatuses_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdeaStatuses_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaderResponses_ReviewerId",
                table: "LeaderResponses",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaStatuses_StatusId",
                table: "IdeaStatuses",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaderResponses_AspNetUsers_ReviewerId",
                table: "LeaderResponses",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
