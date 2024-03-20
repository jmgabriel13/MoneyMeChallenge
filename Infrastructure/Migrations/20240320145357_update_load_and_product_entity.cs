using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_load_and_product_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0e79362e-a6d3-42d8-b374-50c0bfded2ee"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9746015e-6628-4658-a5fb-3ea520549412"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e487dea5-90a1-498e-9666-1508b9be669a"));

            migrationBuilder.RenameColumn(
                name: "InterestRate",
                table: "Products",
                newName: "PerAnnumInterestRate");

            migrationBuilder.RenameColumn(
                name: "MonthlyRepayment",
                table: "LoanApplicatons",
                newName: "Repayment");

            migrationBuilder.AddColumn<int>(
                name: "RepaymentFrequency",
                table: "LoanApplicatons",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "RepaymentFrequency",
                table: "LoanApplicatons");

            migrationBuilder.RenameColumn(
                name: "PerAnnumInterestRate",
                table: "Products",
                newName: "InterestRate");

            migrationBuilder.RenameColumn(
                name: "Repayment",
                table: "LoanApplicatons",
                newName: "MonthlyRepayment");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "InterestRate", "Name" },
                values: new object[,]
                {
                    { new Guid("0e79362e-a6d3-42d8-b374-50c0bfded2ee"), 0m, "Product A" },
                    { new Guid("9746015e-6628-4658-a5fb-3ea520549412"), 6.2m, "Product B" },
                    { new Guid("e487dea5-90a1-498e-9666-1508b9be669a"), 8.3m, "Product C" }
                });
        }
    }
}
