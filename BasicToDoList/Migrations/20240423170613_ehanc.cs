using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicToDoList.Migrations
{
    /// <inheritdoc />
    public partial class ehanc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLoggedIn",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MissionStatusId",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MissionStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Missions_MissionStatusId",
                table: "Missions",
                column: "MissionStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_MissionStatus_MissionStatusId",
                table: "Missions",
                column: "MissionStatusId",
                principalTable: "MissionStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_MissionStatus_MissionStatusId",
                table: "Missions");

            migrationBuilder.DropTable(
                name: "MissionStatus");

            migrationBuilder.DropIndex(
                name: "IX_Missions_MissionStatusId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "IsLoggedIn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MissionStatusId",
                table: "Missions");
        }
    }
}
