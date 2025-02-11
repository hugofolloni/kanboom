using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class SettingUniquePair : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardUser_Board_Fk_BoardId",
                table: "BoardUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardUser_User_Fk_UserId",
                table: "BoardUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_User_Fk_UserId",
                table: "UserGroup");

            migrationBuilder.DropIndex(
                name: "IX_UserGroup_Fk_UserId",
                table: "UserGroup");

            migrationBuilder.DropIndex(
                name: "IX_BoardUser_Fk_UserId",
                table: "BoardUser");

            migrationBuilder.AlterColumn<long>(
                name: "Fk_UserId",
                table: "UserGroup",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Fk_GroupId",
                table: "UserGroup",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Fk_UserId",
                table: "BoardUser",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Fk_BoardId",
                table: "BoardUser",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_Fk_UserId_Fk_GroupId",
                table: "UserGroup",
                columns: new[] { "Fk_UserId", "Fk_GroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoardUser_Fk_UserId_Fk_BoardId",
                table: "BoardUser",
                columns: new[] { "Fk_UserId", "Fk_BoardId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUser_Board_Fk_BoardId",
                table: "BoardUser",
                column: "Fk_BoardId",
                principalTable: "Board",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUser_User_Fk_UserId",
                table: "BoardUser",
                column: "Fk_UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_User_Fk_UserId",
                table: "UserGroup",
                column: "Fk_UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardUser_Board_Fk_BoardId",
                table: "BoardUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardUser_User_Fk_UserId",
                table: "BoardUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_User_Fk_UserId",
                table: "UserGroup");

            migrationBuilder.DropIndex(
                name: "IX_UserGroup_Fk_UserId_Fk_GroupId",
                table: "UserGroup");

            migrationBuilder.DropIndex(
                name: "IX_BoardUser_Fk_UserId_Fk_BoardId",
                table: "BoardUser");

            migrationBuilder.AlterColumn<long>(
                name: "Fk_UserId",
                table: "UserGroup",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Fk_GroupId",
                table: "UserGroup",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Fk_UserId",
                table: "BoardUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Fk_BoardId",
                table: "BoardUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_Fk_UserId",
                table: "UserGroup",
                column: "Fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardUser_Fk_UserId",
                table: "BoardUser",
                column: "Fk_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUser_Board_Fk_BoardId",
                table: "BoardUser",
                column: "Fk_BoardId",
                principalTable: "Board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardUser_User_Fk_UserId",
                table: "BoardUser",
                column: "Fk_UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_User_Fk_UserId",
                table: "UserGroup",
                column: "Fk_UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
