using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 16, 14, 24, 2, 185, DateTimeKind.Local).AddTicks(3247), new DateTime(2023, 9, 16, 14, 24, 2, 185, DateTimeKind.Local).AddTicks(3257) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 16, 14, 9, 43, 208, DateTimeKind.Local).AddTicks(801), new DateTime(2023, 9, 16, 14, 9, 43, 208, DateTimeKind.Local).AddTicks(811) });
        }
    }
}
