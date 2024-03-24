using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_loan_entity_term_to_decimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("27b12ed1-92f9-4a03-8242-282581cc55cd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2b065d0a-9c86-462e-aac1-6c8d9cab89de"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9e6287bf-c582-4665-a847-37748d01fb6b"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Term",
                table: "Loans",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "MinimumDuration", "MonthsOfFreeInterest", "Name", "PerAnnumInterestRate" },
                values: new object[,]
                {
                    { new Guid("21ed7847-e021-4680-97ff-98a2dd28e7ec"), 1, 0, "Product C", 10.58m },
                    { new Guid("76bbd944-5783-49f4-8ec0-51a5585a39d5"), 1, 0, "Product A", 0m },
                    { new Guid("8d646eba-bfd7-4472-a2bb-817a34f49230"), 6, 2, "Product B", 9.20m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("21ed7847-e021-4680-97ff-98a2dd28e7ec"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("76bbd944-5783-49f4-8ec0-51a5585a39d5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8d646eba-bfd7-4472-a2bb-817a34f49230"));

            migrationBuilder.AlterColumn<int>(
                name: "Term",
                table: "Loans",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "MinimumDuration", "MonthsOfFreeInterest", "Name", "PerAnnumInterestRate" },
                values: new object[,]
                {
                    { new Guid("27b12ed1-92f9-4a03-8242-282581cc55cd"), 1, 0, "Product C", 10.58m },
                    { new Guid("2b065d0a-9c86-462e-aac1-6c8d9cab89de"), 1, 2, "Product B", 9.20m },
                    { new Guid("9e6287bf-c582-4665-a847-37748d01fb6b"), 1, 0, "Product A", 0m }
                });
        }
    }
}
