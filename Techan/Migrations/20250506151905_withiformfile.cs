using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Techan.Migrations
{
    /// <inheritdoc />
    public partial class withiformfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Sliders",
                newName: "ImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Sliders",
                newName: "ImageUrl");
        }
    }
}
