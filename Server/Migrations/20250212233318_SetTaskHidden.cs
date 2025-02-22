using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class SetTaskHidden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Board_Fk_Board",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_User_Fk_UserAssignee",
                table: "Task");

            migrationBuilder.AlterColumn<long>(
                name: "Fk_UserAssignee",
                table: "Task",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "Fk_Board",
                table: "Task",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "Task",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Board_Fk_Board",
                table: "Task",
                column: "Fk_Board",
                principalTable: "Board",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_User_Fk_UserAssignee",
                table: "Task",
                column: "Fk_UserAssignee",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Board_Fk_Board",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_User_Fk_UserAssignee",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "Task");

            migrationBuilder.AlterColumn<long>(
                name: "Fk_UserAssignee",
                table: "Task",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Fk_Board",
                table: "Task",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Board_Fk_Board",
                table: "Task",
                column: "Fk_Board",
                principalTable: "Board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_User_Fk_UserAssignee",
                table: "Task",
                column: "Fk_UserAssignee",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
