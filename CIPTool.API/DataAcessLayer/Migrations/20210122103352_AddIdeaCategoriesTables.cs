using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class AddIdeaCategoriesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdeaStatuses_Ideas_IdeaId",
                table: "IdeaStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IdeaStatuses_Statuses_StatusId",
                table: "IdeaStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdeaStatuses",
                table: "IdeaStatuses");

            migrationBuilder.DropIndex(
                name: "IX_IdeaStatuses_IdeaId",
                table: "IdeaStatuses");

            migrationBuilder.DropColumn(
                name: "Award",
                table: "Bonuses");

            migrationBuilder.DropColumn(
                name: "CorrectionFactor",
                table: "Bonuses");

            migrationBuilder.DropColumn(
                name: "IsSolutionPreviouslyDiscussed",
                table: "Bonuses");

            migrationBuilder.DropColumn(
                name: "LowerLimit",
                table: "Bonuses");

            migrationBuilder.DropColumn(
                name: "UpperLimit",
                table: "Bonuses");

            migrationBuilder.RenameColumn(
                name: "StatusText",
                table: "Statuses",
                newName: "Text");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "IdeaStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdeaId",
                table: "IdeaStatuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CorrectionFactor",
                table: "BonusCorrectionFactors",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdeaStatuses",
                table: "IdeaStatuses",
                columns: new[] { "IdeaId", "StatusId" });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdeaCategories",
                columns: table => new
                {
                    IdeaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaCategories", x => new { x.IdeaId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_IdeaCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdeaCategories_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "BonusCorrectionFactors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CorrectionFactor",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "BonusCorrectionFactors",
                keyColumn: "Id",
                keyValue: 2,
                column: "CorrectionFactor",
                value: 0.5m);

            migrationBuilder.CreateIndex(
                name: "IX_IdeaCategories_CategoryId",
                table: "IdeaCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaStatuses_Ideas_IdeaId",
                table: "IdeaStatuses",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaStatuses_Statuses_StatusId",
                table: "IdeaStatuses",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdeaStatuses_Ideas_IdeaId",
                table: "IdeaStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IdeaStatuses_Statuses_StatusId",
                table: "IdeaStatuses");

            migrationBuilder.DropTable(
                name: "IdeaCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdeaStatuses",
                table: "IdeaStatuses");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Statuses",
                newName: "StatusText");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "IdeaStatuses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdeaId",
                table: "IdeaStatuses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<decimal>(
                name: "Award",
                table: "Bonuses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<float>(
                name: "CorrectionFactor",
                table: "Bonuses",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "IsSolutionPreviouslyDiscussed",
                table: "Bonuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "LowerLimit",
                table: "Bonuses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UpperLimit",
                table: "Bonuses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "CorrectionFactor",
                table: "BonusCorrectionFactors",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdeaStatuses",
                table: "IdeaStatuses",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "BonusCorrectionFactors",
                keyColumn: "Id",
                keyValue: 1,
                column: "CorrectionFactor",
                value: 1.0);

            migrationBuilder.UpdateData(
                table: "BonusCorrectionFactors",
                keyColumn: "Id",
                keyValue: 2,
                column: "CorrectionFactor",
                value: 0.5);

            migrationBuilder.CreateIndex(
                name: "IX_IdeaStatuses_IdeaId",
                table: "IdeaStatuses",
                column: "IdeaId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaStatuses_Ideas_IdeaId",
                table: "IdeaStatuses",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaStatuses_Statuses_StatusId",
                table: "IdeaStatuses",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
