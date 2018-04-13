using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class RenameBoatTypeToBoatTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boats_BoatType_BoatTypeId",
                table: "Boats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoatType",
                table: "BoatType");

            migrationBuilder.RenameTable(
                name: "BoatType",
                newName: "BoatTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoatTypes",
                table: "BoatTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_BoatTypes_BoatTypeId",
                table: "Boats",
                column: "BoatTypeId",
                principalTable: "BoatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boats_BoatTypes_BoatTypeId",
                table: "Boats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoatTypes",
                table: "BoatTypes");

            migrationBuilder.RenameTable(
                name: "BoatTypes",
                newName: "BoatType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoatType",
                table: "BoatType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_BoatType_BoatTypeId",
                table: "Boats",
                column: "BoatTypeId",
                principalTable: "BoatType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
