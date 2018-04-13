using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class RemovePlayerBoat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boats_Players_PlayerId",
                table: "Boats");

            migrationBuilder.DropTable(
                name: "PlayerBoats");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Boats",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_Players_PlayerId",
                table: "Boats",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boats_Players_PlayerId",
                table: "Boats");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Boats",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "PlayerBoats",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    BoatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerBoats", x => new { x.PlayerId, x.BoatId });
                    table.ForeignKey(
                        name: "FK_PlayerBoats_Boats_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerBoats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBoats_BoatId",
                table: "PlayerBoats",
                column: "BoatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_Players_PlayerId",
                table: "Boats",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
