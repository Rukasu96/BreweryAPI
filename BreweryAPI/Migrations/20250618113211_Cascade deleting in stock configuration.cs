using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryAPI.Migrations
{
    /// <inheritdoc />
    public partial class Cascadedeletinginstockconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_CompanyAccount_CompanyAccountId",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_CompanyAccount_CompanyAccountId",
                table: "Stocks",
                column: "CompanyAccountId",
                principalTable: "CompanyAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_CompanyAccount_CompanyAccountId",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_CompanyAccount_CompanyAccountId",
                table: "Stocks",
                column: "CompanyAccountId",
                principalTable: "CompanyAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
