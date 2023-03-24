using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Day12Test1.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Taxi",
                columns: table => new
                {
                    TaxiId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaxiName = table.Column<string>(type: "TEXT", nullable: false),
                    IsBusy = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxi", x => x.TaxiId);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaxiId = table.Column<int>(type: "INTEGER", nullable: false),
                    RaceName = table.Column<string>(type: "TEXT", nullable: false),
                    RaceDestination = table.Column<string>(type: "TEXT", nullable: false),
                    KmRace = table.Column<decimal>(type: "TEXT", nullable: false),
                    RaceDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.RaceId);
                    table.ForeignKey(
                        name: "FK_Races_Taxi_TaxiId",
                        column: x => x.TaxiId,
                        principalTable: "Taxi",
                        principalColumn: "TaxiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Races_TaxiId",
                table: "Races",
                column: "TaxiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Taxi");
        }
    }
}
