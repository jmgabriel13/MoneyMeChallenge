using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_loan_application_frequency_prop_to_string : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("32965560-bf68-4fd9-8518-f9908b9f7cff"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("568580a9-fdf0-4ff2-9f99-f7f674adef43"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c01185ea-0ce9-4517-a469-7c1aa68f6e3c"));

            migrationBuilder.AlterColumn<string>(
                name: "RepaymentFrequency",
                table: "LoanApplicatons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "RepaymentFrequency",
                table: "LoanApplicatons",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "PerAnnumInterestRate" },
                values: new object[,]
                {
                    { new Guid("32965560-bf68-4fd9-8518-f9908b9f7cff"), "Product B", 9.20m },
                    { new Guid("568580a9-fdf0-4ff2-9f99-f7f674adef43"), "Product A", 0m },
                    { new Guid("c01185ea-0ce9-4517-a469-7c1aa68f6e3c"), "Product C", 10.58m }
                });
        }
    }
}
