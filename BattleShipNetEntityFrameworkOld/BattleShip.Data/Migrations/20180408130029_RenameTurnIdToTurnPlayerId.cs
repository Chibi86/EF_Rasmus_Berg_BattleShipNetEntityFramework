using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class RenameTurnIdToTurnPlayerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoards_Players_TurnId",
                table: "GameBoards");

            migrationBuilder.RenameColumn(
                name: "TurnId",
                table: "GameBoards",
                newName: "TurnPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameBoards_TurnId",
                table: "GameBoards",
                newName: "IX_GameBoards_TurnPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoards_Players_TurnPlayerId",
                table: "GameBoards",
                column: "TurnPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoards_Players_TurnPlayerId",
                table: "GameBoards");

            migrationBuilder.RenameColumn(
                name: "TurnPlayerId",
                table: "GameBoards",
                newName: "TurnId");

            migrationBuilder.RenameIndex(
                name: "IX_GameBoards_TurnPlayerId",
                table: "GameBoards",
                newName: "IX_GameBoards_TurnId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoards_Players_TurnId",
                table: "GameBoards",
                column: "TurnId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
