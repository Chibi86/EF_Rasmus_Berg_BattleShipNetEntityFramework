using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class FixPlayerGameBoardKeyFromIntToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GameBoardKey",
                table: "Players",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GameBoardKey",
                table: "Players",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
