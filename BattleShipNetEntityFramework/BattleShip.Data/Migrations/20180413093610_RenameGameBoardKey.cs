using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class RenameGameBoardKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GameBoards_GameBoardGameKey",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_GameBoardGameKey",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameBoardGameKey",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "GameKey",
                table: "GameBoards",
                newName: "Key");

            migrationBuilder.AlterColumn<string>(
                name: "GameBoardKey",
                table: "Players",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameBoardKey",
                table: "Players",
                column: "GameBoardKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GameBoards_GameBoardKey",
                table: "Players",
                column: "GameBoardKey",
                principalTable: "GameBoards",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_GameBoards_GameBoardKey",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_GameBoardKey",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "GameBoards",
                newName: "GameKey");

            migrationBuilder.AlterColumn<string>(
                name: "GameBoardKey",
                table: "Players",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GameBoardGameKey",
                table: "Players",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameBoardGameKey",
                table: "Players",
                column: "GameBoardGameKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_GameBoards_GameBoardGameKey",
                table: "Players",
                column: "GameBoardGameKey",
                principalTable: "GameBoards",
                principalColumn: "GameKey",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
