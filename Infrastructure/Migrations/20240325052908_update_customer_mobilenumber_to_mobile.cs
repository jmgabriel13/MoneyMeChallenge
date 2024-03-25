using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class update_customer_mobilenumber_to_mobile : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "MobileNumber",
            table: "Customers",
            newName: "Mobile");

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

        migrationBuilder.InsertData(
            table: "BlacklistedEmailDomains",
            columns: new[] { "Id", "Value" },
            values: new object[,]
            {
                { new Guid("92714ef4-ccd7-402b-97ef-63d32e17691b"), "hotmail.com" },
                { new Guid("b0227712-5aba-45ba-a35c-3020a5cb7ee4"), "blackmail.com" },
                { new Guid("fdc9f665-d109-48bb-b8ec-60135f1713d7"), "test.com" }
            });

        migrationBuilder.InsertData(
            table: "BlacklistedMobileNumbers",
            columns: new[] { "Id", "Value" },
            values: new object[,]
            {
                { new Guid("02df9a39-6764-42fa-ad97-032b51b069ac"), "12312312312" },
                { new Guid("04d21f2d-86cb-42e9-821f-f2b17df26d18"), "09123456789" },
                { new Guid("1041a13a-73f3-4361-a967-e3ac7ce12d62"), "09987654321" }
            });

        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "EstablishmentFee", "MinimumDuration", "MonthsOfFreeInterest", "Name", "PerAnnumInterestRate" },
            values: new object[,]
            {
                { new Guid("150b4159-8439-49c3-90d1-99ce1a3c3e85"), 300, 6, 2, "Product B", 9.20m },
                { new Guid("32d02406-fcde-4a94-9fb0-321713a4f6fe"), 300, 1, 0, "Product C", 10.58m },
                { new Guid("e4e0f5db-6fbe-4233-946c-cbb67e4df578"), 300, 1, 0, "Product A", 0m }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "BlacklistedEmailDomains",
            keyColumn: "Id",
            keyValue: new Guid("92714ef4-ccd7-402b-97ef-63d32e17691b"));

        migrationBuilder.DeleteData(
            table: "BlacklistedEmailDomains",
            keyColumn: "Id",
            keyValue: new Guid("b0227712-5aba-45ba-a35c-3020a5cb7ee4"));

        migrationBuilder.DeleteData(
            table: "BlacklistedEmailDomains",
            keyColumn: "Id",
            keyValue: new Guid("fdc9f665-d109-48bb-b8ec-60135f1713d7"));

        migrationBuilder.DeleteData(
            table: "BlacklistedMobileNumbers",
            keyColumn: "Id",
            keyValue: new Guid("02df9a39-6764-42fa-ad97-032b51b069ac"));

        migrationBuilder.DeleteData(
            table: "BlacklistedMobileNumbers",
            keyColumn: "Id",
            keyValue: new Guid("04d21f2d-86cb-42e9-821f-f2b17df26d18"));

        migrationBuilder.DeleteData(
            table: "BlacklistedMobileNumbers",
            keyColumn: "Id",
            keyValue: new Guid("1041a13a-73f3-4361-a967-e3ac7ce12d62"));

        migrationBuilder.DeleteData(
            table: "Products",
            keyColumn: "Id",
            keyValue: new Guid("150b4159-8439-49c3-90d1-99ce1a3c3e85"));

        migrationBuilder.DeleteData(
            table: "Products",
            keyColumn: "Id",
            keyValue: new Guid("32d02406-fcde-4a94-9fb0-321713a4f6fe"));

        migrationBuilder.DeleteData(
            table: "Products",
            keyColumn: "Id",
            keyValue: new Guid("e4e0f5db-6fbe-4233-946c-cbb67e4df578"));

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
}
