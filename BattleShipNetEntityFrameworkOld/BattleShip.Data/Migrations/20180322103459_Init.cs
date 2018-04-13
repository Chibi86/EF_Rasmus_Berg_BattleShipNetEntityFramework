using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BattleShip.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoatType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameBoards",
                columns: table => new
                {
                    GameKey = table.Column<string>(nullable: false),
                    AccountId = table.Column<int>(nullable: true),
                    Player1Id = table.Column<int>(nullable: false),
                    Player2Id = table.Column<int>(nullable: false),
                    Private = table.Column<bool>(nullable: false),
                    Turn = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameBoards", x => x.GameKey);
                    table.ForeignKey(
                        name: "FK_GameBoards_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    GameBoardGameKey = table.Column<string>(nullable: true),
                    GameBoardId = table.Column<int>(nullable: false),
                    HaveSeenEndScreen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_GameBoards_GameBoardGameKey",
                        column: x => x.GameBoardGameKey,
                        principalTable: "GameBoards",
                        principalColumn: "GameKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Boats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoatTypeId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boats_BoatType_BoatTypeId",
                        column: x => x.BoatTypeId,
                        principalTable: "BoatType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerHits",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerHits", x => new { x.PlayerId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_PlayerHits_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerHits_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoatHits",
                columns: table => new
                {
                    BoatID = table.Column<int>(nullable: false),
                    PositionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatHits", x => new { x.BoatID, x.PositionID });
                    table.ForeignKey(
                        name: "FK_BoatHits_Boats_BoatID",
                        column: x => x.BoatID,
                        principalTable: "Boats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoatHits_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerBoats",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    BoatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerBoats", x => new { x.PlayerId, x.BoatId });
                    table.ForeignKey(
                        name: "FK_PlayerBoats_Boats_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerBoats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoatHits_PositionID",
                table: "BoatHits",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Boats_BoatTypeId",
                table: "Boats",
                column: "BoatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Boats_PlayerId",
                table: "Boats",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameBoards_AccountId",
                table: "GameBoards",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBoats_BoatId",
                table: "PlayerBoats",
                column: "BoatId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHits_PositionId",
                table: "PlayerHits",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_AccountId",
                table: "Players",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameBoardGameKey",
                table: "Players",
                column: "GameBoardGameKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoatHits");

            migrationBuilder.DropTable(
                name: "PlayerBoats");

            migrationBuilder.DropTable(
                name: "PlayerHits");

            migrationBuilder.DropTable(
                name: "Boats");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "BoatType");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "GameBoards");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
