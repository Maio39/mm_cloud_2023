using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Day9Todo.Migrations
{
    /// <inheritdoc />
    public partial class ProvaDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Tasks");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Tasks",
                type: "DATETIME",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
