using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryAPI.Migrations
{
    /// <inheritdoc />
    public partial class ShopBaskets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_ShopBaskets_ShopBasketId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ShopBasketId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ShopBasketId",
                table: "Stocks");

            migrationBuilder.AlterColumn<Guid>(
                name: "WholesalerId",
                table: "ShopBaskets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "BeerId",
                table: "ShopBaskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BeerId1",
                table: "ShopBaskets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ShopBaskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShopBaskets_BeerId",
                table: "ShopBaskets",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopBaskets_BeerId1",
                table: "ShopBaskets",
                column: "BeerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopBaskets_Beers_BeerId",
                table: "ShopBaskets",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopBaskets_Beers_BeerId1",
                table: "ShopBaskets",
                column: "BeerId1",
                principalTable: "Beers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopBaskets_Beers_BeerId",
                table: "ShopBaskets");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopBaskets_Beers_BeerId1",
                table: "ShopBaskets");

            migrationBuilder.DropIndex(
                name: "IX_ShopBaskets_BeerId",
                table: "ShopBaskets");

            migrationBuilder.DropIndex(
                name: "IX_ShopBaskets_BeerId1",
                table: "ShopBaskets");

            migrationBuilder.DropColumn(
                name: "BeerId",
                table: "ShopBaskets");

            migrationBuilder.DropColumn(
                name: "BeerId1",
                table: "ShopBaskets");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ShopBaskets");

            migrationBuilder.AddColumn<int>(
                name: "ShopBasketId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "WholesalerId",
                table: "ShopBaskets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ShopBasketId",
                table: "Stocks",
                column: "ShopBasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_ShopBaskets_ShopBasketId",
                table: "Stocks",
                column: "ShopBasketId",
                principalTable: "ShopBaskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
