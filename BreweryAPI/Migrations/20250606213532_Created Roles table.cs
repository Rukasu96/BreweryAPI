using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BreweryAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreatedRolestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Breweries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Role" },
                values: new object[,]
                {
                    { 1, "Brewery" },
                    { 2, "Wholesaler" },
                    { 3, "Client" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Breweries_RoleId",
                table: "Breweries",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Breweries_Roles_RoleId",
                table: "Breweries",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breweries_Roles_RoleId",
                table: "Breweries");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Breweries_RoleId",
                table: "Breweries");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Breweries");
        }
    }
}
