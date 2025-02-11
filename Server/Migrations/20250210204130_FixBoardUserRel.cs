using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class FixBoardUserRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardUser_Board_Fk_UserId",
                table: "BoardUser");

            migrationBuilder.CreateIndex(
                name: "IX_BoardUser_Fk_BoardId",
                table: "BoardUser",
                column: "Fk_BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUser_Board_Fk_BoardId",
                table: "BoardUser",
                column: "Fk_BoardId",
                principalTable: "Board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardUser_Board_Fk_BoardId",
                table: "BoardUser");

            migrationBuilder.DropIndex(
                name: "IX_BoardUser_Fk_BoardId",
                table: "BoardUser");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUser_Board_Fk_UserId",
                table: "BoardUser",
                column: "Fk_UserId",
                principalTable: "Board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
