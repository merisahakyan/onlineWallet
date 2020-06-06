using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineWallet.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currenies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currenies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Currenies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currenies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currenies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "USD" },
                    { 30, "SGD" },
                    { 29, "PHP" },
                    { 28, "NZD" },
                    { 27, "MYR" },
                    { 26, "MXN" },
                    { 25, "KRW" },
                    { 24, "INR" },
                    { 23, "ILS" },
                    { 22, "IDR" },
                    { 21, "HKD" },
                    { 20, "CNY" },
                    { 19, "CAD" },
                    { 18, "BRL" },
                    { 17, "AUD" },
                    { 16, "TRY" },
                    { 15, "RUB" },
                    { 14, "HRK" },
                    { 13, "NOK" },
                    { 12, "ISK" },
                    { 11, "CHF" },
                    { 10, "SEK" },
                    { 9, "RON" },
                    { 8, "PLN" },
                    { 7, "HUF" },
                    { 6, "GBP" },
                    { 5, "DKK" },
                    { 4, "CZK" },
                    { 3, "BGN" },
                    { 2, "JPY" },
                    { 31, "THB" },
                    { 32, "ZAR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CurrencyId",
                table: "Wallets",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Currenies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
