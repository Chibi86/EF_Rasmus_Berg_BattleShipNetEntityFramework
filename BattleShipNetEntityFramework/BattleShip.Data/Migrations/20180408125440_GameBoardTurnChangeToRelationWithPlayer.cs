using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class GameBoardTurnChangeToRelationWithPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Turn",
                table: "GameBoards",
                newName: "TurnId");

            migrationBuilder.CreateIndex(
                name: "IX_GameBoards_TurnId",
                table: "GameBoards",
                column: "TurnId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoards_Players_TurnId",
                table: "GameBoards",
                column: "TurnId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoards_Players_TurnId",
                table: "GameBoards");

            migrationBuilder.DropIndex(
                name: "IX_GameBoards_TurnId",
                table: "GameBoards");

            migrationBuilder.RenameColumn(
                name: "TurnId",
                table: "GameBoards",
                newName: "Turn");
        }
    }
}
