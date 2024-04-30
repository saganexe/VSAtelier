using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VSAtelier.Migrations.ForumDb
{
    /// <inheritdoc />
    public partial class forum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    textForm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    imagePhoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewForumOGId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forums_Forums_NewForumOGId",
                        column: x => x.NewForumOGId,
                        principalTable: "Forums",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forums_NewForumOGId",
                table: "Forums",
                column: "NewForumOGId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forums");
        }
    }
}
