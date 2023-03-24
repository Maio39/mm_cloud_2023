using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Day10SerialKiller.Migrations
{
    /// <inheritdoc />
    public partial class SecondInitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SerialKillers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsInJail",
                table: "SerialKillers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Killing",
                table: "SerialKillers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SerialKillers");

            migrationBuilder.DropColumn(
                name: "IsInJail",
                table: "SerialKillers");

            migrationBuilder.DropColumn(
                name: "Killing",
                table: "SerialKillers");
        }
    }
}
