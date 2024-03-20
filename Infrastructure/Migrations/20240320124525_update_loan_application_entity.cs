using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_loan_application_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("394d760e-2e45-49d5-8b88-3a17def101e4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("79bcb127-1afe-4c2a-af8f-6482cf362841"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d480dc5b-bb75-4ab9-a2de-f764d2d72dc0"));

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "LoanApplicatons");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LoanApplicatons",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LoanApplicatons");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "LoanApplicatons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "InterestRate", "Name" },
                values: new object[,]
                {
                    { new Guid("394d760e-2e45-49d5-8b88-3a17def101e4"), 0m, "Product A" },
                    { new Guid("79bcb127-1afe-4c2a-af8f-6482cf362841"), 6.2m, "Product B" },
                    { new Guid("d480dc5b-bb75-4ab9-a2de-f764d2d72dc0"), 8.3m, "Product C" }
                });
        }
    }
}
