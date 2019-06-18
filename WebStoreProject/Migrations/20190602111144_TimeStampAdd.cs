using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStoreProject.Migrations
{
    public partial class TimeStampAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1L,
                column: "Date",
                value: new DateTime(2019, 6, 2, 14, 11, 43, 643, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2L,
                column: "Date",
                value: new DateTime(2019, 6, 2, 14, 11, 43, 644, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3L,
                column: "Date",
                value: new DateTime(2019, 6, 2, 14, 11, 43, 644, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "BirthDate",
                value: new DateTime(2019, 6, 2, 14, 11, 43, 639, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "BirthDate",
                value: new DateTime(2019, 6, 2, 14, 11, 43, 641, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1L,
                column: "Date",
                value: new DateTime(2019, 5, 31, 12, 25, 54, 266, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2L,
                column: "Date",
                value: new DateTime(2019, 5, 31, 12, 25, 54, 266, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3L,
                column: "Date",
                value: new DateTime(2019, 5, 31, 12, 25, 54, 266, DateTimeKind.Local));

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
    }
}
