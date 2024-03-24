using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_seeding_for_products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BlacklistedEmailDomains",
                keyColumn: "Id",
                keyValue: new Guid("3fc3b1f6-be87-4383-9d8c-0f9811002028"));

            migrationBuilder.DeleteData(
                table: "BlacklistedEmailDomains",
                keyColumn: "Id",
                keyValue: new Guid("aad99a98-ba65-4509-bb5c-6a1209305144"));

            migrationBuilder.DeleteData(
                table: "BlacklistedEmailDomains",
                keyColumn: "Id",
                keyValue: new Guid("dea374b9-0c86-44d6-84a7-add33623dfea"));

            migrationBuilder.DeleteData(
                table: "BlacklistedMobileNumbers",
                keyColumn: "Id",
                keyValue: new Guid("18868832-8f77-4ca3-81aa-0542a78ec4d4"));

            migrationBuilder.DeleteData(
                table: "BlacklistedMobileNumbers",
                keyColumn: "Id",
                keyValue: new Guid("5a5e06a6-feb4-4032-aab8-2ddaccd58231"));

            migrationBuilder.DeleteData(
                table: "BlacklistedMobileNumbers",
                keyColumn: "Id",
                keyValue: new Guid("6e2969dc-94a4-4f9a-884d-e931a0e557df"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("71f76f0e-b8b9-4062-9170-af75144d7755"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b7f0cc4c-02d4-4a64-b5bf-9b496f15aaf4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b913c05b-9fa8-4701-a6ab-e93717566d69"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "BlacklistedEmailDomains",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("3fc3b1f6-be87-4383-9d8c-0f9811002028"), "blackmail.com" },
                    { new Guid("aad99a98-ba65-4509-bb5c-6a1209305144"), "test.com" },
                    { new Guid("dea374b9-0c86-44d6-84a7-add33623dfea"), "hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "BlacklistedMobileNumbers",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("18868832-8f77-4ca3-81aa-0542a78ec4d4"), "09123456789" },
                    { new Guid("5a5e06a6-feb4-4032-aab8-2ddaccd58231"), "09987654321" },
                    { new Guid("6e2969dc-94a4-4f9a-884d-e931a0e557df"), "12312312312" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "MinimumDuration", "MonthsOfFreeInterest", "Name", "PerAnnumInterestRate" },
                values: new object[,]
                {
                    { new Guid("71f76f0e-b8b9-4062-9170-af75144d7755"), 1, 0, "Product C", 10.58m },
                    { new Guid("b7f0cc4c-02d4-4a64-b5bf-9b496f15aaf4"), 6, 2, "Product B", 9.20m },
                    { new Guid("b913c05b-9fa8-4701-a6ab-e93717566d69"), 1, 0, "Product A", 0m }
                });
        }
    }
}
