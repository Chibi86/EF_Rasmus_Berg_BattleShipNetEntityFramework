using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class GameBoardsFromTwoCellToListOfPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Player1Id",
                table: "GameBoards");

            migrationBuilder.DropColumn(
                name: "Player2Id",
                table: "GameBoards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Player1Id",
                table: "GameBoards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Player2Id",
                table: "GameBoards",
                nullable: false,
                defaultValue: 0);
        }
    }
}
