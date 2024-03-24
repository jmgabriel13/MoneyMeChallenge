using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_loan_entity_add_term_in_months : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TermInMonths",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "MinimumDuration", "MonthsOfFreeInterest", "Name", "PerAnnumInterestRate" },
                values: new object[,]
                {
                    { new Guid("3e712e8d-1362-4ef4-bf30-d5cf947ca630"), 1, 0, "Product C", 10.58m },
                    { new Guid("b4bdbbe5-ad86-4288-9274-0a63195ffb04"), 1, 0, "Product A", 0m },
                    { new Guid("fa6bf24b-cf0b-4cbd-b783-1547cf64a886"), 6, 2, "Product B", 9.20m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3e712e8d-1362-4ef4-bf30-d5cf947ca630"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b4bdbbe5-ad86-4288-9274-0a63195ffb04"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fa6bf24b-cf0b-4cbd-b783-1547cf64a886"));

            migrationBuilder.DropColumn(
                name: "TermInMonths",
                table: "Loans");

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
    }
}
