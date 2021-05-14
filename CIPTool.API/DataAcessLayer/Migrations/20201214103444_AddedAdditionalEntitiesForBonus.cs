using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class AddedAdditionalEntitiesForBonus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialReports_Bonuses_BonusId",
                table: "FinancialReports");

            migrationBuilder.DropIndex(
                name: "IX_FinancialReports_BonusId",
                table: "FinancialReports");

            migrationBuilder.AddColumn<int>(
                name: "BonusCorrectionFactorId",
                table: "Bonuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusRangeId",
                table: "Bonuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "FinancialReportId",
                table: "Bonuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BonusCorrectionFactors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectionFactor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusCorrectionFactors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BonusRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LowerBound = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpperBound = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Award = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusRanges", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BonusCorrectionFactors",
                columns: new[] { "Id", "CorrectionFactor", "Text" },
                values: new object[,]
                {
                    { 1, 1.0, "never discussed in the organization" },
                    { 2, 0.5, "previously discussed in the organization" }
                });

            migrationBuilder.InsertData(
                table: "BonusRanges",
                columns: new[] { "Id", "Award", "LowerBound", "UpperBound" },
                values: new object[,]
                {
                    { 1, 50.0m, 500.0m, 1000.0m },
                    { 2, 90.0m, 1000.0m, 2000.0m },
                    { 3, 160.0m, 2000.0m, 3000.0m },
                    { 4, 300.0m, 3000.0m, 6000.0m },
                    { 5, 500.0m, 10000.0m, 20000.0m },
                    { 6, 900.0m, 20000.0m, 30000.0m },
                    { 7, 1100.0m, 30000.0m, 40000.0m },
                    { 8, 1200.0m, 40000.0m, 50000.0m },
                    { 9, 1500.0m, 50000.0m, 1000000.0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_BonusCorrectionFactorId",
                table: "Bonuses",
                column: "BonusCorrectionFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_BonusRangeId",
                table: "Bonuses",
                column: "BonusRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_FinancialReportId",
                table: "Bonuses",
                column: "FinancialReportId",
                unique: true,
                filter: "[FinancialReportId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_BonusCorrectionFactors_BonusCorrectionFactorId",
                table: "Bonuses",
                column: "BonusCorrectionFactorId",
                principalTable: "BonusCorrectionFactors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_BonusRanges_BonusRangeId",
                table: "Bonuses",
                column: "BonusRangeId",
                principalTable: "BonusRanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuses_FinancialReports_FinancialReportId",
                table: "Bonuses",
                column: "FinancialReportId",
                principalTable: "FinancialReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_BonusCorrectionFactors_BonusCorrectionFactorId",
                table: "Bonuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_BonusRanges_BonusRangeId",
                table: "Bonuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Bonuses_FinancialReports_FinancialReportId",
                table: "Bonuses");

            migrationBuilder.DropTable(
                name: "BonusCorrectionFactors");

            migrationBuilder.DropTable(
                name: "BonusRanges");

            migrationBuilder.DropIndex(
                name: "IX_Bonuses_BonusCorrectionFactorId",
                table: "Bonuses");

            migrationBuilder.DropIndex(
                name: "IX_Bonuses_BonusRangeId",
                table: "Bonuses");

            migrationBuilder.DropIndex(
                name: "IX_Bonuses_FinancialReportId",
                table: "Bonuses");

            migrationBuilder.DropColumn(
                name: "BonusCorrectionFactorId",
                table: "Bonuses");

            migrationBuilder.DropColumn(
                name: "BonusRangeId",
                table: "Bonuses");

            migrationBuilder.DropColumn(
                name: "FinancialReportId",
                table: "Bonuses");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialReports_BonusId",
                table: "FinancialReports",
                column: "BonusId",
                unique: true,
                filter: "[BonusId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialReports_Bonuses_BonusId",
                table: "FinancialReports",
                column: "BonusId",
                principalTable: "Bonuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
