using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_product_entity_add_establishmentfee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BlacklistedEmailDomains",
                keyColumn: "Id",
                keyValue: new Guid("13588b8b-5c2c-42b3-b848-55d8465e9b38"));

            migrationBuilder.DeleteData(
                table: "BlacklistedEmailDomains",
                keyColumn: "Id",
                keyValue: new Guid("23ad3782-63ef-46da-abf9-d5dec5649d97"));

            migrationBuilder.DeleteData(
                table: "BlacklistedEmailDomains",
                keyColumn: "Id",
                keyValue: new Guid("9bd7888d-ed04-4b34-9240-9d723705c797"));

            migrationBuilder.DeleteData(
                table: "BlacklistedMobileNumbers",
                keyColumn: "Id",
                keyValue: new Guid("407f22ac-a270-4e9f-a18b-45fe7a844a42"));

            migrationBuilder.DeleteData(
                table: "BlacklistedMobileNumbers",
                keyColumn: "Id",
                keyValue: new Guid("8fdf415c-1568-4bde-a89b-412b2cf7aba9"));

            migrationBuilder.DeleteData(
                table: "BlacklistedMobileNumbers",
                keyColumn: "Id",
                keyValue: new Guid("c40c2e7e-b532-47ae-9c93-9e19c88a1195"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("70bffe4a-bf53-4361-ab3b-f90b0ba6f201"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("864e4c22-8cee-4857-9138-f5c4fd9d13f1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f46d09a9-0af1-4a0d-b177-d8e42887a7d7"));

            migrationBuilder.AddColumn<int>(
                name: "EstablishmentFee",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "BlacklistedEmailDomains",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("7f0dc9c8-21a0-46c1-ba10-77337298209b"), "hotmail.com" },
                    { new Guid("b1b387bf-f98c-4eab-ad3b-5fc571abe6bb"), "blackmail.com" },
                    { new Guid("b3d8fe81-4db9-417e-bd49-01a877fbc1d0"), "test.com" }
                });

            migrationBuilder.InsertData(
                table: "BlacklistedMobileNumbers",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("1173c7e6-02e5-4089-b189-bddde36a59e7"), "09123456789" },
                    { new Guid("8297a80d-ada4-4e9b-acd5-caa6441ad457"), "12312312312" },
                    { new Guid("df13aaa4-c796-4859-9f24-cbbbebe1bf86"), "09987654321" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "EstablishmentFee", "MinimumDuration", "MonthsOfFreeInterest", "Name", "PerAnnumInterestRate" },
                values: new object[,]
                {
                    { new Guid("cbd45508-0a66-41ef-a424-6e14e1a6fb50"), 300, 1, 0, "Product A", 0m },
                    { new Guid("cf1b644a-0c76-4c3b-b577-70374d8edc21"), 300, 1, 0, "Product C", 10.58m },
                    { new Guid("d1a7ca5f-6f67-4600-9e01-19758c70fc09"), 300, 6, 2, "Product B", 9.20m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BlacklistedEmailDomains",
                keyColumn: "Id",
                keyValue: new Guid("7f0dc9c8-21a0-46c1-ba10-77337298209b"));

            migrationBuilder.DeleteData(
                table: "BlacklistedEmailDomains",
                keyColumn: "Id",
                keyValue: new Guid("b1b387bf-f98c-4eab-ad3b-5fc571abe6bb"));

            migrationBuilder.DeleteData(
                table: "BlacklistedEmailDomains",
                keyColumn: "Id",
                keyValue: new Guid("b3d8fe81-4db9-417e-bd49-01a877fbc1d0"));

            migrationBuilder.DeleteData(
                table: "BlacklistedMobileNumbers",
                keyColumn: "Id",
                keyValue: new Guid("1173c7e6-02e5-4089-b189-bddde36a59e7"));

            migrationBuilder.DeleteData(
                table: "BlacklistedMobileNumbers",
                keyColumn: "Id",
                keyValue: new Guid("8297a80d-ada4-4e9b-acd5-caa6441ad457"));

            migrationBuilder.DeleteData(
                table: "BlacklistedMobileNumbers",
                keyColumn: "Id",
                keyValue: new Guid("df13aaa4-c796-4859-9f24-cbbbebe1bf86"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cbd45508-0a66-41ef-a424-6e14e1a6fb50"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cf1b644a-0c76-4c3b-b577-70374d8edc21"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d1a7ca5f-6f67-4600-9e01-19758c70fc09"));

            migrationBuilder.DropColumn(
                name: "EstablishmentFee",
                table: "Products");

            migrationBuilder.InsertData(
                table: "BlacklistedEmailDomains",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("13588b8b-5c2c-42b3-b848-55d8465e9b38"), "test.com" },
                    { new Guid("23ad3782-63ef-46da-abf9-d5dec5649d97"), "blackmail.com" },
                    { new Guid("9bd7888d-ed04-4b34-9240-9d723705c797"), "hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "BlacklistedMobileNumbers",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("407f22ac-a270-4e9f-a18b-45fe7a844a42"), "09123456789" },
                    { new Guid("8fdf415c-1568-4bde-a89b-412b2cf7aba9"), "12312312312" },
                    { new Guid("c40c2e7e-b532-47ae-9c93-9e19c88a1195"), "09987654321" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "MinimumDuration", "MonthsOfFreeInterest", "Name", "PerAnnumInterestRate" },
                values: new object[,]
                {
                    { new Guid("70bffe4a-bf53-4361-ab3b-f90b0ba6f201"), 1, 0, "Product A", 0m },
                    { new Guid("864e4c22-8cee-4857-9138-f5c4fd9d13f1"), 6, 2, "Product B", 9.20m },
                    { new Guid("f46d09a9-0af1-4a0d-b177-d8e42887a7d7"), 1, 0, "Product C", 10.58m }
                });
        }
    }
}
