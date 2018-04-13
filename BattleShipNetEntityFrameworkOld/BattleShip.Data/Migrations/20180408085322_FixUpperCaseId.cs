using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class FixUpperCaseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatHits_Boats_BoatID",
                table: "BoatHits");

            migrationBuilder.DropForeignKey(
                name: "FK_BoatHits_Positions_PositionID",
                table: "BoatHits");

            migrationBuilder.DropForeignKey(
                name: "FK_BoatPositions_Boats_BoatID",
                table: "BoatPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_BoatPositions_Positions_PositionID",
                table: "BoatPositions");

            migrationBuilder.RenameColumn(
                name: "GameBoardId",
                table: "Players",
                newName: "GameBoardKey");

            migrationBuilder.RenameColumn(
                name: "PositionID",
                table: "BoatPositions",
                newName: "PositionId");

            migrationBuilder.RenameColumn(
                name: "BoatID",
                table: "BoatPositions",
                newName: "BoatId");

            migrationBuilder.RenameIndex(
                name: "IX_BoatPositions_PositionID",
                table: "BoatPositions",
                newName: "IX_BoatPositions_PositionId");

            migrationBuilder.RenameColumn(
                name: "PositionID",
                table: "BoatHits",
                newName: "PositionId");

            migrationBuilder.RenameColumn(
                name: "BoatID",
                table: "BoatHits",
                newName: "BoatId");

            migrationBuilder.RenameIndex(
                name: "IX_BoatHits_PositionID",
                table: "BoatHits",
                newName: "IX_BoatHits_PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoatHits_Boats_BoatId",
                table: "BoatHits",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoatHits_Positions_PositionId",
                table: "BoatHits",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoatPositions_Boats_BoatId",
                table: "BoatPositions",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoatPositions_Positions_PositionId",
                table: "BoatPositions",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatHits_Boats_BoatId",
                table: "BoatHits");

            migrationBuilder.DropForeignKey(
                name: "FK_BoatHits_Positions_PositionId",
                table: "BoatHits");

            migrationBuilder.DropForeignKey(
                name: "FK_BoatPositions_Boats_BoatId",
                table: "BoatPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_BoatPositions_Positions_PositionId",
                table: "BoatPositions");

            migrationBuilder.RenameColumn(
                name: "GameBoardKey",
                table: "Players",
                newName: "GameBoardId");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "BoatPositions",
                newName: "PositionID");

            migrationBuilder.RenameColumn(
                name: "BoatId",
                table: "BoatPositions",
                newName: "BoatID");

            migrationBuilder.RenameIndex(
                name: "IX_BoatPositions_PositionId",
                table: "BoatPositions",
                newName: "IX_BoatPositions_PositionID");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "BoatHits",
                newName: "PositionID");

            migrationBuilder.RenameColumn(
                name: "BoatId",
                table: "BoatHits",
                newName: "BoatID");

            migrationBuilder.RenameIndex(
                name: "IX_BoatHits_PositionId",
                table: "BoatHits",
                newName: "IX_BoatHits_PositionID");

            migrationBuilder.AddForeignKey(
                name: "FK_BoatHits_Boats_BoatID",
                table: "BoatHits",
                column: "BoatID",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoatHits_Positions_PositionID",
                table: "BoatHits",
                column: "PositionID",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoatPositions_Boats_BoatID",
                table: "BoatPositions",
                column: "BoatID",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoatPositions_Positions_PositionID",
                table: "BoatPositions",
                column: "PositionID",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
