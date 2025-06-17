using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryAPI.Migrations
{
    /// <inheritdoc />
    public partial class Changenameproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Stocks_StockId",
                table: "Beers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAccount_Stocks_stockId",
                table: "CompanyAccount");

            migrationBuilder.RenameColumn(
                name: "stockId",
                table: "CompanyAccount",
                newName: "StockId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyAccount_stockId",
                table: "CompanyAccount",
                newName: "IX_CompanyAccount_StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Stocks_StockId",
                table: "Beers",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAccount_Stocks_StockId",
                table: "CompanyAccount",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Stocks_StockId",
                table: "Beers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAccount_Stocks_StockId",
                table: "CompanyAccount");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "CompanyAccount",
                newName: "stockId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyAccount_StockId",
                table: "CompanyAccount",
                newName: "IX_CompanyAccount_stockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Stocks_StockId",
                table: "Beers",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAccount_Stocks_stockId",
                table: "CompanyAccount",
                column: "stockId",
                principalTable: "Stocks",
                principalColumn: "Id");
        }
    }
}
