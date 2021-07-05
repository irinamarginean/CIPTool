using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class AddedResponsibleForImplementationInIdeaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsibleId",
                table: "Ideas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_ResponsibleId",
                table: "Ideas",
                column: "ResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_AspNetUsers_ResponsibleId",
                table: "Ideas",
                column: "ResponsibleId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_AspNetUsers_ResponsibleId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_ResponsibleId",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Ideas");
        }
    }
}
