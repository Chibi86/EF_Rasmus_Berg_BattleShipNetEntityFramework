using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class RemoveAccountFromGameBoard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameBoards_Accounts_AccountId",
                table: "GameBoards");

            migrationBuilder.DropIndex(
                name: "IX_GameBoards_AccountId",
                table: "GameBoards");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "GameBoards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "GameBoards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameBoards_AccountId",
                table: "GameBoards",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameBoards_Accounts_AccountId",
                table: "GameBoards",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
