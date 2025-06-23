using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryAPI.Migrations
{
    /// <inheritdoc />
    public partial class relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_ShopBaskets_ShopBasketId",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_ShopBaskets_ShopBasketId",
                table: "Stocks",
                column: "ShopBasketId",
                principalTable: "ShopBaskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_ShopBaskets_ShopBasketId",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_ShopBaskets_ShopBasketId",
                table: "Stocks",
                column: "ShopBasketId",
                principalTable: "ShopBaskets",
                principalColumn: "Id");
        }
    }
}
