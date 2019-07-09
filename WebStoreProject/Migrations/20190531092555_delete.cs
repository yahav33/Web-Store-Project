using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStoreProject.Migrations
{
    public partial class Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1L,
                columns: new[] { "Date", "OwnerId" },
                values: new object[] { new DateTime(2019, 5, 31, 12, 25, 54, 266, DateTimeKind.Local), 2L });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2L,
                columns: new[] { "Date", "OwnerId" },
                values: new object[] { new DateTime(2019, 5, 31, 12, 25, 54, 266, DateTimeKind.Local), 2L });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3L,
                columns: new[] { "Date", "OwnerId" },
                values: new object[] { new DateTime(2019, 5, 31, 12, 25, 54, 266, DateTimeKind.Local), 2L });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "BirthDate",
                value: new DateTime(2019, 5, 31, 12, 25, 54, 262, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "BirthDate",
                value: new DateTime(2019, 5, 31, 12, 25, 54, 264, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1L,
                columns: new[] { "Date", "OwnerId" },
                values: new object[] { new DateTime(2019, 5, 30, 13, 18, 33, 544, DateTimeKind.Local), 1L });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2L,
                columns: new[] { "Date", "OwnerId" },
                values: new object[] { new DateTime(2019, 5, 30, 13, 18, 33, 544, DateTimeKind.Local), 1L });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3L,
                columns: new[] { "Date", "OwnerId" },
                values: new object[] { new DateTime(2019, 5, 30, 13, 18, 33, 544, DateTimeKind.Local), 1L });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "BirthDate",
                value: new DateTime(2019, 5, 30, 13, 18, 33, 540, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "BirthDate",
                value: new DateTime(2019, 5, 30, 13, 18, 33, 542, DateTimeKind.Local));
        }
    }
}
