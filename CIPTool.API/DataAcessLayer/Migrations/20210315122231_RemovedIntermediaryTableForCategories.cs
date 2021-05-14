using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class RemovedIntermediaryTableForCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdeaCategories_Categories_CategoryId",
                table: "IdeaCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_IdeaCategories_Ideas_IdeaId",
                table: "IdeaCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdeaCategories",
                table: "IdeaCategories");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "IdeaStatuses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "IdeaStatuses");

            migrationBuilder.RenameTable(
                name: "IdeaCategories",
                newName: "IdeaCategory");

            migrationBuilder.RenameColumn(
                name: "TargetDate",
                table: "IdeaStatuses",
                newName: "Date");

            migrationBuilder.RenameIndex(
                name: "IX_IdeaCategories_CategoryId",
                table: "IdeaCategory",
                newName: "IX_IdeaCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdeaCategory",
                table: "IdeaCategory",
                columns: new[] { "IdeaId", "CategoryId" });

            migrationBuilder.CreateTable(
                name: "CategoryIdeaEntity",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    IdeasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryIdeaEntity", x => new { x.CategoriesId, x.IdeasId });
                    table.ForeignKey(
                        name: "FK_CategoryIdeaEntity_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryIdeaEntity_Ideas_IdeasId",
                        column: x => x.IdeasId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryIdeaEntity_IdeasId",
                table: "CategoryIdeaEntity",
                column: "IdeasId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaCategory_Categories_CategoryId",
                table: "IdeaCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaCategory_Ideas_IdeaId",
                table: "IdeaCategory",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdeaCategory_Categories_CategoryId",
                table: "IdeaCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_IdeaCategory_Ideas_IdeaId",
                table: "IdeaCategory");

            migrationBuilder.DropTable(
                name: "CategoryIdeaEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdeaCategory",
                table: "IdeaCategory");

            migrationBuilder.RenameTable(
                name: "IdeaCategory",
                newName: "IdeaCategories");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "IdeaStatuses",
                newName: "TargetDate");

            migrationBuilder.RenameIndex(
                name: "IX_IdeaCategory_CategoryId",
                table: "IdeaCategories",
                newName: "IX_IdeaCategories_CategoryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "IdeaStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "IdeaStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdeaCategories",
                table: "IdeaCategories",
                columns: new[] { "IdeaId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaCategories_Categories_CategoryId",
                table: "IdeaCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaCategories_Ideas_IdeaId",
                table: "IdeaCategories",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
