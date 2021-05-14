using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class AddedReviewerInLeaderResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<string>(
                name: "ReviewerId",
                table: "LeaderResponses",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "LeaderResponses");

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Text" },
                values: new object[,]
                {
                    { 1, "Plan" },
                    { 2, "Do" },
                    { 3, "Act" },
                    { 4, "Check" }
                });
        }
    }
}
