using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    GroupLink = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Fk_OwnerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fk_UserId = table.Column<long>(type: "bigint", nullable: false),
                    Fk_GroupId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroup_User_Fk_UserId",
                        column: x => x.Fk_UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Board",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StagesCount = table.Column<int>(type: "integer", nullable: false),
                    Fk_GroupId = table.Column<long>(type: "bigint", nullable: true),
                    Fk_BoardOwner = table.Column<long>(type: "bigint", nullable: false),
                    IsGroupBoard = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Board", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Board_Group_Fk_GroupId",
                        column: x => x.Fk_GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BoardUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fk_UserId = table.Column<long>(type: "bigint", nullable: false),
                    Fk_BoardId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardUser_Board_Fk_UserId",
                        column: x => x.Fk_UserId,
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardUser_User_Fk_UserId",
                        column: x => x.Fk_UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StageLevels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StageName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Fk_Board = table.Column<long>(type: "bigint", nullable: false),
                    StageNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageLevels_Board_Fk_Board",
                        column: x => x.Fk_Board,
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Fk_Board = table.Column<long>(type: "bigint", nullable: false),
                    StageNumber = table.Column<int>(type: "integer", nullable: false),
                    Fk_UserAssignee = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Board_Fk_Board",
                        column: x => x.Fk_Board,
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_User_Fk_UserAssignee",
                        column: x => x.Fk_UserAssignee,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Board_Fk_GroupId",
                table: "Board",
                column: "Fk_GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardUser_Fk_UserId",
                table: "BoardUser",
                column: "Fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StageLevels_Fk_Board",
                table: "StageLevels",
                column: "Fk_Board");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Fk_Board",
                table: "Task",
                column: "Fk_Board");

            migrationBuilder.CreateIndex(
                name: "IX_Task_Fk_UserAssignee",
                table: "Task",
                column: "Fk_UserAssignee");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_Fk_UserId",
                table: "UserGroup",
                column: "Fk_UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardUser");

            migrationBuilder.DropTable(
                name: "StageLevels");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "Board");

            migrationBuilder.DropTable(
                name: "Group");
        }
    }
}
