using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcessLayer.Migrations
{
    public partial class AddedMissingBonusRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 300.0m, 6000.0m, 10000.0m });

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

            migrationBuilder.InsertData(
                table: "BonusRanges",
                columns: new[] { "Id", "Award", "LowerBound", "UpperBound" },
                values: new object[] { 10, 1500.0m, 50000.0m, 1000000.0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 500.0m, 10000.0m, 20000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 900.0m, 20000.0m, 30000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 1100.0m, 30000.0m, 40000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 1200.0m, 40000.0m, 50000.0m });

            migrationBuilder.UpdateData(
                table: "BonusRanges",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Award", "LowerBound", "UpperBound" },
                values: new object[] { 1500.0m, 50000.0m, 1000000.0m });
        }
    }
}
