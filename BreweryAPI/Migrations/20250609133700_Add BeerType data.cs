using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BreweryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBeerTypedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breweries_Roles_RoleId",
                table: "Breweries");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Breweries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "BeerTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Pils" },
                    { 2, "Ale" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Breweries_Roles_RoleId",
                table: "Breweries",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breweries_Roles_RoleId",
                table: "Breweries");

            migrationBuilder.DeleteData(
                table: "BeerTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BeerTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Breweries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Breweries_Roles_RoleId",
                table: "Breweries",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
