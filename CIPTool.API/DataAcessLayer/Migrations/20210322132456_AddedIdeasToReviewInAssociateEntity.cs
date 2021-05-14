using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class AddedIdeasToReviewInAssociateEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewerId",
                table: "Ideas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_ReviewerId",
                table: "Ideas",
                column: "ReviewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_AspNetUsers_ReviewerId",
                table: "Ideas",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_AspNetUsers_ReviewerId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_ReviewerId",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "Ideas");
        }
    }
}
