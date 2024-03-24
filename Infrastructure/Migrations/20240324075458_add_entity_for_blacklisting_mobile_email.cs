using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_entity_for_blacklisting_mobile_email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BlacklistedEmailDomains",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlacklistedEmailDomains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlacklistedMobileNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlacklistedMobileNumbers", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlacklistedEmailDomains");

            migrationBuilder.DropTable(
                name: "BlacklistedMobileNumbers");

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
                table: "Products",
                columns: new[] { "Id", "MinimumDuration", "MonthsOfFreeInterest", "Name", "PerAnnumInterestRate" },
                values: new object[,]
                {
                    { new Guid("3e712e8d-1362-4ef4-bf30-d5cf947ca630"), 1, 0, "Product C", 10.58m },
                    { new Guid("b4bdbbe5-ad86-4288-9274-0a63195ffb04"), 1, 0, "Product A", 0m },
                    { new Guid("fa6bf24b-cf0b-4cbd-b783-1547cf64a886"), 6, 2, "Product B", 9.20m }
                });
        }
    }
}
