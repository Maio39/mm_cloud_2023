using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Day9Todo.Migrations
{
    /// <inheritdoc />
    public partial class ThirdDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("IsComplete", "Tasks");
            migrationBuilder.AddColumn<bool>("IsComplete", "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
