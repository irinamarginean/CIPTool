using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialReports_Bonuses_BonusId",
                table: "FinancialReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_FinancialReports_FinancialReportId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_FinancialReportId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_FinancialReports_BonusId",
                table: "FinancialReports");

            migrationBuilder.AlterColumn<Guid>(
                name: "FinancialReportId",
                table: "Ideas",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdeaId",
                table: "FinancialReports",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BonusId",
                table: "FinancialReports",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialReports_BonusId",
                table: "FinancialReports",
                column: "BonusId",
                unique: true,
                filter: "[BonusId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialReports_IdeaId",
                table: "FinancialReports",
                column: "IdeaId",
                unique: true,
                filter: "[IdeaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialReports_Bonuses_BonusId",
                table: "FinancialReports",
                column: "BonusId",
                principalTable: "Bonuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialReports_Ideas_IdeaId",
                table: "FinancialReports",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialReports_Bonuses_BonusId",
                table: "FinancialReports");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialReports_Ideas_IdeaId",
                table: "FinancialReports");

            migrationBuilder.DropIndex(
                name: "IX_FinancialReports_BonusId",
                table: "FinancialReports");

            migrationBuilder.DropIndex(
                name: "IX_FinancialReports_IdeaId",
                table: "FinancialReports");

            migrationBuilder.AlterColumn<Guid>(
                name: "FinancialReportId",
                table: "Ideas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdeaId",
                table: "FinancialReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BonusId",
                table: "FinancialReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_FinancialReportId",
                table: "Ideas",
                column: "FinancialReportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialReports_BonusId",
                table: "FinancialReports",
                column: "BonusId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialReports_Bonuses_BonusId",
                table: "FinancialReports",
                column: "BonusId",
                principalTable: "Bonuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_FinancialReports_FinancialReportId",
                table: "Ideas",
                column: "FinancialReportId",
                principalTable: "FinancialReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
