using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FavoriteMovies.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class MovieApplicationUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ApplicationUserId",
                table: "Movies",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_ApplicationUserId",
                table: "Movies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_ApplicationUserId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ApplicationUserId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Movies");
        }
    }
}
