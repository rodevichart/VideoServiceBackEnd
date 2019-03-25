using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoServiceDAL.Migrations
{
    public partial class ReplaceManyToManyToOneToOneOnMovieService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Covers_Movies_MovieId",
                table: "Covers");

            migrationBuilder.DropIndex(
                name: "IX_Covers_MovieId",
                table: "Covers");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Covers");

            migrationBuilder.AddColumn<long>(
                name: "CoverId",
                table: "Movies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CoverId",
                table: "Movies",
                column: "CoverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Covers_CoverId",
                table: "Movies",
                column: "CoverId",
                principalTable: "Covers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Covers_CoverId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CoverId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CoverId",
                table: "Movies");

            migrationBuilder.AddColumn<long>(
                name: "MovieId",
                table: "Covers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Covers_MovieId",
                table: "Covers",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Covers_Movies_MovieId",
                table: "Covers",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
