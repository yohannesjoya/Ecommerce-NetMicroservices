using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AddressLine", "CVV", "CardName", "CardNumber", "Country", "CreatedBy", "CreatedDate", "EmailAddress", "Expiration", "FirstName", "LastModifiedBy", "LastModifiedDate", "LastName", "PaymentMethod", "State", "TotalPrice", "UserName", "ZipCode" },
                values: new object[] { -1, "123 Main St", "123", "John Doe", "4111111111111111", "United States", "AdminUser", new DateTime(2023, 9, 16, 14, 24, 2, 185, DateTimeKind.Local).AddTicks(3247), "john.doe@example.com", "12/25", "John", "AdminUser", new DateTime(2023, 9, 16, 14, 24, 2, 185, DateTimeKind.Local).AddTicks(3257), "Doe", 1, "California", 99.99m, "JohnDoe123", "90001" });
        }
    }
}
