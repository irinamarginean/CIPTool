using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class AddedAssociateEntityInLeaderResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_LeaderResponses_ReviewerId",
                table: "LeaderResponses",
                column: "ReviewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaderResponses_AspNetUsers_ReviewerId",
                table: "LeaderResponses",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaderResponses_AspNetUsers_ReviewerId",
                table: "LeaderResponses");

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
    }
}
