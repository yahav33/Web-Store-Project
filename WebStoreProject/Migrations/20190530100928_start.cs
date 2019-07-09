using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStoreProject.Migrations
{
    public partial class Start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Level = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OwnerID = table.Column<long>(nullable: true),
                    UserID = table.Column<long>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: false),
                    LongDescription = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ImageOne = table.Column<byte[]>(nullable: true),
                    ImageTwo = table.Column<byte[]>(nullable: true),
                    ImageThree = table.Column<byte[]>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "BirthDate", "Email", "FirstName", "LastName", "Level", "Password", "UserName" },
                values: new object[] { 1L, new DateTime(2019, 5, 30, 13, 9, 27, 761, DateTimeKind.Local), "Heyyy@gmail.com", "nice car", "cool", (byte)1, "10", "fsd" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "BirthDate", "Email", "FirstName", "LastName", "Level", "Password", "UserName" },
                values: new object[] { 2L, new DateTime(2019, 5, 30, 13, 9, 27, 763, DateTimeKind.Local), "Heyyy@gmail.com", "nice car", "cool", (byte)1, "10", "fsd" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Date", "ImageOne", "ImageThree", "ImageTwo", "LongDescription", "OwnerID", "Price", "ShortDescription", "Status", "Title", "UserID" },
                values: new object[] { 1L, new DateTime(2019, 5, 30, 13, 9, 27, 764, DateTimeKind.Local), null, null, null, "nice car", 1L, 10m, "cool", 0, "Ferari", 1L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Date", "ImageOne", "ImageThree", "ImageTwo", "LongDescription", "OwnerID", "Price", "ShortDescription", "Status", "Title", "UserID" },
                values: new object[] { 2L, new DateTime(2019, 5, 30, 13, 9, 27, 764, DateTimeKind.Local), null, null, null, "cool car", 1L, 100m, "nice", 0, "Lamburgini", 1L });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Date", "ImageOne", "ImageThree", "ImageTwo", "LongDescription", "OwnerID", "Price", "ShortDescription", "Status", "Title", "UserID" },
                values: new object[] { 3L, new DateTime(2019, 5, 30, 13, 9, 27, 764, DateTimeKind.Local), null, null, null, "Dope car", 1L, 1200m, "Epic", 0, "Bugatti", 2L });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserID",
                table: "Products",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
