using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryAPI.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAccount_Stocks_StockId",
                table: "CompanyAccount");

            migrationBuilder.DropIndex(
                name: "IX_CompanyAccount_StockId",
                table: "CompanyAccount");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "CompanyAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_CompanyAccountId",
                table: "Stocks",
                column: "CompanyAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_CompanyAccount_CompanyAccountId",
                table: "Stocks",
                column: "CompanyAccountId",
                principalTable: "CompanyAccount",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_CompanyAccount_CompanyAccountId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_CompanyAccountId",
                table: "Stocks");

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "CompanyAccount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAccount_StockId",
                table: "CompanyAccount",
                column: "StockId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAccount_Stocks_StockId",
                table: "CompanyAccount",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id");
        }
    }
}
