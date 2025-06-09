using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreweryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedwholesalertableconfigurationbeerWholesalerrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wholesalers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(12)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wholesalers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wholesalers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Wholesalers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BeerWholesalers",
                columns: table => new
                {
                    BeerId = table.Column<int>(type: "int", nullable: false),
                    WholesalerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerWholesalers", x => new { x.BeerId, x.WholesalerId });
                    table.ForeignKey(
                        name: "FK_BeerWholesalers_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerWholesalers_Wholesalers_WholesalerId",
                        column: x => x.WholesalerId,
                        principalTable: "Wholesalers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeerWholesalers_WholesalerId",
                table: "BeerWholesalers",
                column: "WholesalerId");

            migrationBuilder.CreateIndex(
                name: "IX_Wholesalers_AddressId",
                table: "Wholesalers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Wholesalers_RoleId",
                table: "Wholesalers",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerWholesalers");

            migrationBuilder.DropTable(
                name: "Wholesalers");
        }
    }
}
