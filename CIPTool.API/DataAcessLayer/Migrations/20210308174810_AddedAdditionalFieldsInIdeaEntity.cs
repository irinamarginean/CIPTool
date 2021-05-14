using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class AddedAdditionalFieldsInIdeaEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RichTextDescription",
                table: "Ideas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Ideas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 0.0m, -10000000.0m, 500.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 50.0m, 500.0m, 1000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 90.0m, 1000.0m, 2000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 160.0m, 2000.0m, 3000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "LowerBound", "UpperBound" },
                values: new object[] { 3000.0m, 6000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 300.0m, 6000.0m, 10000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 500.0m, 10000.0m, 20000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 900.0m, 20000.0m, 30000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 1100.0m, 30000.0m, 40000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 1200.0m, 40000.0m, 50000.0m });

            migrationBuilder.InsertData(
                table: "BonusRanges",
                columns: new[] { "Id", "Award", "LowerBound", "UpperBound" },
                values: new object[] { 11, 1500.0m, 50000.0m, 10000000.0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "RichTextDescription",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Ideas");

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 50.0m, 500.0m, 1000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 90.0m, 1000.0m, 2000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 160.0m, 2000.0m, 3000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 300.0m, 3000.0m, 6000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "LowerBound", "UpperBound" },
                values: new object[] { 6000.0m, 10000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 500.0m, 10000.0m, 20000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 900.0m, 20000.0m, 30000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 1100.0m, 30000.0m, 40000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 1200.0m, 40000.0m, 50000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 1500.0m, 50000.0m, 1000000.0m });
        }
    }
}
