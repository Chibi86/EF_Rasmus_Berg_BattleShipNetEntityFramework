using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class AddBoatTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("BoatTypes", new string[] { "Id", "Name", "Size" }, new object[,] {
                { 1, "Battleship", 5 },
                { 2, "Cruiser", 4 },
                { 3, "Destroyer", 3 },
                { 4, "Submarine", 2 }
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"TRUNCATE TABLE BoatTypes");
        }
    }
}
