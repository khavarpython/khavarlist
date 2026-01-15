using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace khavarlist.Migrations
{
    /// <inheritdoc />
    public partial class AddDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "UserMangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "UserAnimes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "UserMangas");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "UserAnimes");
        }
    }
}
