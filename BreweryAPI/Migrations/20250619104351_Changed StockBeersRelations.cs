using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedStockBeersRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE TABLE #TempStocks (
                Id INT,
                BeerId INT,
                -- Add other columns from Stocks table here
            );
        ");

            migrationBuilder.Sql(@"
            INSERT INTO #TempStocks
            SELECT Id, BeerId
            FROM Stocks
            WHERE BeerId NOT IN (SELECT Id FROM Beers);
        ");

            migrationBuilder.Sql(@"
            DELETE FROM Stocks
            WHERE BeerId NOT IN (SELECT Id FROM Beers);
        ");

            migrationBuilder.AlterColumn<int>(
                name: "BeerId",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.Sql(@"
            ALTER TABLE Stocks CHECK CONSTRAINT ALL;
        ");

            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Stocks_StockId",
                table: "Beers");

            migrationBuilder.DropIndex(
                name: "IX_Beers_StockId",
                table: "Beers");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_BeerId",
                table: "Stocks",
                column: "BeerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Beers_BeerId",
                table: "Stocks",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER TABLE Stocks NOCHECK CONSTRAINT ALL;
        ");

            migrationBuilder.AlterColumn<int>(
                name: "BeerId",
                table: "Stocks",
                type: "int",
                nullable: false);

            migrationBuilder.Sql(@"
            ALTER TABLE Stocks CHECK CONSTRAINT ALL;
        ");
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Beers_BeerId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_BeerId",
                table: "Stocks");

            migrationBuilder.CreateIndex(
                name: "IX_Beers_StockId",
                table: "Beers",
                column: "StockId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Stocks_StockId",
                table: "Beers",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
