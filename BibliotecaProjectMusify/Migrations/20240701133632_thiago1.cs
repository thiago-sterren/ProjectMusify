using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaProjectMusify.Migrations
{
    /// <inheritdoc />
    public partial class thiago1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_Userid",
                table: "Playlists");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "Playlists",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_Userid",
                table: "Playlists",
                newName: "IX_Playlists_userid");

            migrationBuilder.AlterColumn<int>(
                name: "userid",
                table: "Playlists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_userid",
                table: "Playlists",
                column: "userid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_userid",
                table: "Playlists");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Playlists",
                newName: "Userid");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_userid",
                table: "Playlists",
                newName: "IX_Playlists_Userid");

            migrationBuilder.AlterColumn<int>(
                name: "Userid",
                table: "Playlists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_Userid",
                table: "Playlists",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id");
        }
    }
}
