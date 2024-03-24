using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_product_entity_to_be_more_flexible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7788147c-0362-4ff0-83aa-3d6f8efb89db"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ca030e17-1a19-4d94-b2ca-48a578b08f90"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e72414b4-ca00-4688-bcb4-30f4dea15425"));

            migrationBuilder.AddColumn<int>(
                name: "MinimumDuration",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthsOfFreeInterest",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "MinimumDuration",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MonthsOfFreeInterest",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "PerAnnumInterestRate" },
                values: new object[,]
                {
                    { new Guid("7788147c-0362-4ff0-83aa-3d6f8efb89db"), "Product A", 0m },
                    { new Guid("ca030e17-1a19-4d94-b2ca-48a578b08f90"), "Product B", 9.20m },
                    { new Guid("e72414b4-ca00-4688-bcb4-30f4dea15425"), "Product C", 10.58m }
                });
        }
    }
}
