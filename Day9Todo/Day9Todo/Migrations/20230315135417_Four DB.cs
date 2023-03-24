using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Day9Todo.Migrations
{
    /// <inheritdoc />
    public partial class FourDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Updated = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Deadline = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IsComplete = table.Column<bool>(type: "BIT", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
