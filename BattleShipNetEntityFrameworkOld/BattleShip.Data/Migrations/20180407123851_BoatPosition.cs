using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class BoatPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoatPositions",
                columns: table => new
                {
                    BoatID = table.Column<int>(nullable: false),
                    PositionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatPositions", x => new { x.BoatID, x.PositionID });
                    table.ForeignKey(
                        name: "FK_BoatPositions_Boats_BoatID",
                        column: x => x.BoatID,
                        principalTable: "Boats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoatPositions_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoatPositions_PositionID",
                table: "BoatPositions",
                column: "PositionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoatPositions");
        }
    }
}
