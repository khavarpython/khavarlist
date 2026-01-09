using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace khavarlist.Migrations
{
    /// <inheritdoc />
    public partial class AddMangaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    MalId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalChapters = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.MalId);
                });

            migrationBuilder.CreateTable(
                name: "UserMangas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    ReadStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMangas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMangas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMangas_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMangas_MangaId",
                table: "UserMangas",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMangas_UserId_MangaId",
                table: "UserMangas",
                columns: new[] { "UserId", "MangaId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMangas");

            migrationBuilder.DropTable(
                name: "Mangas");
        }
    }
}
