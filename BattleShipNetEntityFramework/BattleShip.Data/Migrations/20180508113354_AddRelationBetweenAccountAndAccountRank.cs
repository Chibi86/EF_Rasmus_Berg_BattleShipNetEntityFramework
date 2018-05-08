using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class AddRelationBetweenAccountAndAccountRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RankId",
                table: "Accounts",
                column: "RankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountRanks_RankId",
                table: "Accounts",
                column: "RankId",
                principalTable: "AccountRanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountRanks_RankId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_RankId",
                table: "Accounts");
        }
    }
}
