using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class NotRequiredPlayerTurnId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoards_Players_TurnPlayerId",
                table: "GameBoards");

            migrationBuilder.AlterColumn<int>(
                name: "TurnPlayerId",
                table: "GameBoards",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoards_Players_TurnPlayerId",
                table: "GameBoards",
                column: "TurnPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoards_Players_TurnPlayerId",
                table: "GameBoards");

            migrationBuilder.AlterColumn<int>(
                name: "TurnPlayerId",
                table: "GameBoards",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoards_Players_TurnPlayerId",
                table: "GameBoards",
                column: "TurnPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
